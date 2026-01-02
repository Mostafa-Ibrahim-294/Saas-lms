using Application.Features.Files.Commands.UploadFile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Files.Dtos
{
    public sealed class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<UploadFileCommand, Domain.Entites.File>();
        }

    }
}
