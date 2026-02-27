using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ModuleItems.Commands.UpdateLesson
{
    public sealed record UpdateLessonCommand(int CourseId, int ModuleId, int ItemId, string Title, string? Description, string VideoId,
        IEnumerable<Resource> Resources) : IRequest<OneOf<SuccessDto, Error>>;
}
