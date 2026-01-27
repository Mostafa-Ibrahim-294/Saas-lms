using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Contracts.Files
{
    public interface IFileService
    {
        long GetMaxSize(FileType fileType);
        string GetPath(string fileId, string name, string? folder, string originalFileName);
        string CreateCdnUrl(string path);
        string CreateUploadUrl(string path);


        FileType GetFileType(string contentType);
        Task<string?> UploadFileAsync(IFormFile file, string path);
        Task<string?> UploadVideoAsync(IFormFile video, string? folder);
        Task<string?> UploadAsync(IFormFile file, string path, string? folder = null);
    }
}
