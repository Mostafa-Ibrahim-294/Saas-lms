using Application.Features.Courses.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Courses.Queries.GetCourseStatistics
{
    public sealed record GetCourseStatisticsQuery(int CourseId) : IRequest<OneOf<CourseStatisticsDto, Error>>;
}
