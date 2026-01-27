using Application.Common;
using Application.Contracts.Files;
using Application.Contracts.Repositories;
using Application.Features.Files.Dtos;
using Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Application.Features.Files.Commands.UploadFile
{
    internal class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, OneOf<UploadFileDto, Error>>
    {
        private readonly IFileService _fileService;
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserId _currentUserId;
        private readonly ILogger<UploadFileCommandHandler> _logger;

        public UploadFileCommandHandler(IFileService fileService, IFileRepository fileRepository, 
            IMapper mapper, ICurrentUserId currentUserId, ILogger<UploadFileCommandHandler> logger)
        {
            _fileService = fileService;
            _fileRepository = fileRepository;
            _mapper = mapper;
            _currentUserId = currentUserId;
            _logger = logger;
        }
        public async Task<OneOf<UploadFileDto, Error>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var fileType = _fileService.GetFileType(request.File.ContentType);
            var fileId = Guid.NewGuid().ToString();
            var path = _fileService.GetPath(fileId, request.Name, request.Folder, request.File.FileName);
            var userId = _currentUserId.GetUserId();

            _logger.LogWarning("Uploading file {FileName} with Id {FileId} for user {UserId}", request.Name, fileId, userId);

            string? cdnUrl = await _fileService.UploadAsync(request.File, path, request.Folder);

            

            if (cdnUrl == null)
                return FileError.UploadFailed;

            var file = new Domain.Entites.File
            {
                Id = fileId,
                Name = request.Name,
                Size = request.File.Length,
                Type = fileType,
                Url = cdnUrl,
                UploadedById = userId,
                Status = request.Embedding?.Enabled == true
                    ? FileStatus.Processing
                    : FileStatus.Success
            };

            await _fileRepository.CreateAsync(file, cancellationToken);
            await _fileRepository.SaveAsync(cancellationToken);

            return new UploadFileDto
            {
                FileId = fileId,
                FileType = fileType.ToString(),
                Url = cdnUrl,
                OriginalName = request.Name,
                Size = request.File.Length
            };
        }
    }
}
