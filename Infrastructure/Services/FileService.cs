using Application.Contracts.Files;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Infrastructure.Services
{
    internal class FileService : IFileService
    {
        private readonly IOptions<Common.Options.FileOptions> _options;
        private readonly IOptions<BunnyOptions> _bunnyOptions;
        public FileService(IOptions<Common.Options.FileOptions> options, IOptions<BunnyOptions> bunnyOptions)
        {
            _options = options;
            _bunnyOptions = bunnyOptions;
        }

        public string CreateCdnUrl(string path)
        {
            var cdnUrl = _bunnyOptions.Value.CdnUrl;
            return $"{cdnUrl}{HttpUtility.UrlPathEncode(path)}";
        }

        public string CreateUploadUrl(string path)
        {
            var hostUrl = _bunnyOptions.Value.HostUrl;
            var storageZoneName = _bunnyOptions.Value.StorageZoneName;
            return $"{hostUrl}{storageZoneName}/{HttpUtility.UrlPathEncode(path)}";
        }

        public (string AccessKey, int ExpiresInMinutes) GetAccessKey()
        {
            return (_bunnyOptions.Value.AccessKey, _bunnyOptions.Value.ExpiryMinutes);
        }

        public long GetMaxSize(FileType fileType) =>
            fileType == FileType.Video ? _options.Value.VideoMaxSize : (fileType == FileType.Image ? _options.Value.ImageMaxSize : _options.Value.DocumentMaxSize);

        public string GetPath(string fileId, string name, string folder)
        {
            var sanitizedFolder = string.IsNullOrWhiteSpace(folder) ? string.Empty : folder.Trim('/');
            var sanitizedFileName = name.Replace(" ", "_");
            return $"{sanitizedFolder}/{fileId}_{sanitizedFileName}";
        }
    }
}
