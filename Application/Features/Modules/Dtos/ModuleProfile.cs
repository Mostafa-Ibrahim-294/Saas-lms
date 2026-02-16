using Application.Features.Modules.Commands.CreateModule;
using Application.Features.Modules.Commands.UpdateModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Modules.Dtos
{
    public sealed class ModuleProfile : Profile
    {
        public ModuleProfile()
        {
            CreateMap<CreateModuleCommand, Module>();
            CreateMap<UpdateModuleCommand, Module>();
        }
    }
}
