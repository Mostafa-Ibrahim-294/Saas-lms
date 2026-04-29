using Application.Features.Assignments.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Assignments.Queries.GetOverview
{
    public sealed record GetOverviewQuery(int CourseId, int ModuleId, int ItemId) : IRequest<OneOf<OverviewDto, Error>>;    
}
