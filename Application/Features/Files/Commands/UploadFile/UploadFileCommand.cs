using Application.Features.Files.Dtos;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Files.Commands.UploadFile
{
    public sealed record UploadFileCommand(IFormFile File, string? Folder, string? Name, EmbeddingDto? Embedding) : IRequest<OneOf<UploadFileDto, Error>>;
}
