using Application.Constants;
using Application.Contracts.Files;
using Application.Features.Files.Dtos;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;

namespace Infrastructure.Services
{
    internal class FileService : IFileService
    {
        private readonly IOptions<Common.Options.FileOptions> _options;
        private readonly IOptions<BunnyOptions> _bunnyOptions;
        private readonly HttpClient _httpClient;
        public FileService(IOptions<Common.Options.FileOptions> options, IOptions<BunnyOptions> bunnyOptions, HttpClient httpClient)
        {
            _options = options;
            _bunnyOptions = bunnyOptions;
            _httpClient = httpClient;
        }

        public string CreateCdnUrl(string path)
        {
            var cdnUrl = _bunnyOptions.Value.CdnUrl;
            var encodedPath = HttpUtility.UrlPathEncode(path);
            return $"{cdnUrl.TrimEnd('/')}/{encodedPath.TrimStart('/')}";
        }
        public long GetMaxSize(FileType fileType) =>
            fileType == FileType.Video ? _options.Value.VideoMaxSize : (fileType == FileType.Image ? _options.Value.ImageMaxSize : _options.Value.DocumentMaxSize);
        public string GetPath(string fileId, string name, string? folder, string originalFileName)
        {
            var sanitizedFolder = string.IsNullOrWhiteSpace(folder) ? string.Empty : folder.Trim('/');
            var sanitizedFileName = name.Replace(" ", "_");

            var extension = Path.GetExtension(originalFileName);

            return string.IsNullOrEmpty(sanitizedFolder)
                ? $"{fileId}_{sanitizedFileName}{extension}"
                : $"{sanitizedFolder}/{fileId}_{sanitizedFileName}{extension}";
        }
        public string CreateUploadUrl(string path)
        {
            var hostName = _bunnyOptions.Value.HostName;
            var storageZoneName = _bunnyOptions.Value.StorageZoneName;
            var encodedPath = HttpUtility.UrlPathEncode(path);
            return $"https://{hostName}/{storageZoneName}/{encodedPath.TrimStart('/')}";
        }



        public FileType GetFileType(string contentType)
        {
            return contentType.StartsWith(FileConstants.Image) ? FileType.Image :
                   contentType.StartsWith(FileConstants.Video) ? FileType.Video :
                   FileType.Document;
        }
        public async Task<string?> UploadFileAsync(IFormFile file, string path)
        {
            var uploadUrl = CreateUploadUrl(path);
            using var stream = file.OpenReadStream();
            using var content = new StreamContent(stream);

            content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

            var request = new HttpRequestMessage(HttpMethod.Put, uploadUrl)
            {
                Content = content
            };
            request.Headers.Add(FileConstants.AccessKey, _bunnyOptions.Value.AccessKey);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return CreateCdnUrl(path);
            else
                return null;
        }
        public async Task<string?> UploadVideoAsync(IFormFile video, string? folder)
        {
            var title = string.IsNullOrWhiteSpace(folder)
                ? Path.GetFileNameWithoutExtension(video.FileName)
                : $"{folder}/{Path.GetFileNameWithoutExtension(video.FileName)}";

            var createRequest = new HttpRequestMessage(HttpMethod.Post,
                $"{FileConstants.BunnyStreamBaseUrl}/{_bunnyOptions.Value.VideoLibraryId}/{FileConstants.Videos}")
            {
                Content = JsonContent.Create(new { title })
            };

            createRequest.Headers.Add(FileConstants.AccessKey, _bunnyOptions.Value.StreamKey);

            var createResponse = await _httpClient.SendAsync(createRequest);
            if (!createResponse.IsSuccessStatusCode)
            {
                var error = await createResponse.Content.ReadAsStringAsync();
                return null;
            }

            var createJson = await createResponse.Content.ReadAsStringAsync();
            var videoInfo = JsonSerializer.Deserialize<StreamUploadResponse>(createJson);

            if (string.IsNullOrWhiteSpace(videoInfo?.guid))
                return null;

            using var stream = video.OpenReadStream();
            var content = new StreamContent(stream);
            content.Headers.ContentType = new MediaTypeHeaderValue(FileConstants.OctetStream);
            content.Headers.ContentLength = video.Length;

            var uploadRequest = new HttpRequestMessage(
                HttpMethod.Put,
                $"{FileConstants.BunnyStreamBaseUrl}/{_bunnyOptions.Value.VideoLibraryId}/{FileConstants.Videos}/{videoInfo.guid}")
            {
                Content = content
            };

            uploadRequest.Headers.Add(FileConstants.AccessKey, _bunnyOptions.Value.StreamKey);

            var uploadResponse = await _httpClient.SendAsync(uploadRequest);
            if (!uploadResponse.IsSuccessStatusCode)
                return null;

            var extension = Path.GetExtension(video.FileName).TrimStart('.');
            return $"{FileConstants.BunnyEmbedBaseUrl}/{_bunnyOptions.Value.VideoLibraryId}/{videoInfo.guid}.{extension}";
        }
        public Task<string?> UploadAsync(IFormFile file, string path, string? folder = null)
        {
            var type = GetFileType(file.ContentType);
            return type == FileType.Video
                ? UploadVideoAsync(file, folder)
                : UploadFileAsync(file, path);
        }
    }
}
