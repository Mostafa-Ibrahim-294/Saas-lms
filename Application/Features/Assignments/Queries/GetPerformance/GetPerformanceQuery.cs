using Application.Features.Assignments.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Assignments.Queries.GetPerformance
{
    public sealed record GetPerformanceQuery(int CourseId, int ModuleId, int ItemId) : IRequest<OneOf<PerformanceDto, Error>>;
}
