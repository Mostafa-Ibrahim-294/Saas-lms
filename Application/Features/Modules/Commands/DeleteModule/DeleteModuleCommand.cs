using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Modules.Commands.DeleteModule
{
    public sealed record DeleteModuleCommand(int CourseId, int ModuleId) : IRequest<OneOf<SuccessDto, Error>>;
}
