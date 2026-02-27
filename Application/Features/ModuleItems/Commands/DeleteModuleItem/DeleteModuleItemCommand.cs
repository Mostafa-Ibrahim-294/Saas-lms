using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ModuleItems.Commands.DeleteModuleItem
{
    public sealed record DeleteModuleItemCommand(int CourseId, int ModuleId, int ItemId) : IRequest<OneOf<SuccessDto, Error>>;
}
