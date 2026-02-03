using Application.Contracts.Files;
using Application.Features.Files.Dtos;

namespace Application.Features.Files.Commands.CreateUpload
{
    internal class CreateUploadCommandHandler : IRequestHandler<CreateUploadCommand, OneOf<CreateUploadDto, Error>>
    {
        private readonly IFileService _fileService;

        public CreateUploadCommandHandler(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<OneOf<CreateUploadDto, Error>> Handle(CreateUploadCommand request, CancellationToken cancellationToken)
        {
            var credentials = await _fileService.CreateUploadCredentialsAsync(request.Title, request.Size ,cancellationToken);
            
            return credentials == null ? FileError.UploadFailed : credentials;
        }
    }
}
