using Application.Common;
using Application.Contracts.Files;
using Application.Contracts.Repositories;
using Application.Features.Files.Dtos;
using Domain.Enums;

namespace Application.Features.Files.Commands.UploadFile
{
    internal class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, OneOf<UploadFileDto, Error>>
    {
        private readonly IFileService _fileService;
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserId _currentUserId;

        public UploadFileCommandHandler(IFileService fileService, IFileRepository fileRepository,
            IMapper mapper, ICurrentUserId currentUserId)
        {
            _fileService = fileService;
            _fileRepository = fileRepository;
            _mapper = mapper;
            _currentUserId = currentUserId;
        }
        public async Task<OneOf<UploadFileDto, Error>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var fileType = _fileService.GetFileType(request.File.ContentType);
            var fileId = Guid.NewGuid().ToString();
            var fileName = request.Name ?? request.File.FileName;
            var path = _fileService.GetPath(fileId, fileName, request.Folder, request.File.FileName);
            var userId = _currentUserId.GetUserId();

            string? cdnUrl = await _fileService.UploadAsync(request.File, path, request.Folder);

            if (cdnUrl == null)
                return FileError.UploadFailed;

            var file = new Domain.Entites.File
            {
                Id = fileId,
                Name = request.Name != null
                    ? request.Name
                    : request.File.FileName,
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
                OriginalName = request.Name != null
                    ? request.Name
                    : request.File.FileName,
                Size = request.File.Length
            };
        }
    }
}
