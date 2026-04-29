using Application.Features.Quizzes.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Quizzes.Queries.GetPerformance
{
    public sealed record GetPerformanceQuery(int CourseId, int ModuleId, int ItemId) : IRequest<OneOf<PerformanceDto, Error>>;
}
