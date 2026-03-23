using Application.Features.Courses.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Courses.Queries.GetModules
{
    public sealed record GetModulesQuery(int CourseId) : IRequest<OneOf<CourseModuleDto, Error>>;
}
