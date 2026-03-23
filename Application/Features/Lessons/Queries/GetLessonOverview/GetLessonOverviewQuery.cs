using Application.Features.Lessons.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Lessons.Queries.GetLessonOverview
{
    public sealed record GetLessonOverviewQuery(int CourseId, int ModuleId, int ItemId) : IRequest<OneOf<LessonOverviewDto, Error>>;
}
