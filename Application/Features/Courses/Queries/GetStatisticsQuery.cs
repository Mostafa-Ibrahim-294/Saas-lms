using Application.Features.Courses.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Courses.Queries
{
    public sealed record GetStatisticsQuery : IRequest<StatisticsDto>;
}
