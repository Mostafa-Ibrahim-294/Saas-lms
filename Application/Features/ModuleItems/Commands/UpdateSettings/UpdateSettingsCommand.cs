using Application.Features.ModuleItems.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ModuleItems.Commands.UpdateSettings
{
    public sealed record UpdateSettingsCommand(int CourseId, int ModuleId, int ItemId, CourseStatus Status, bool AllowDiscussions,
         IEnumerable<ConditionDto> Conditions) : IRequest<OneOf<SuccessDto, Error>>;
}
