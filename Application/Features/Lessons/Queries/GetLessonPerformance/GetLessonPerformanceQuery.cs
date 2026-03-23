using Application.Features.Lessons.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Lessons.Queries.GetLessonPerformance
{
    public sealed record GetLessonPerformanceQuery(int CourseId, int ModuleId, int ItemId) : IRequest<OneOf<LessonPerformanceDto, Error>>;

}
