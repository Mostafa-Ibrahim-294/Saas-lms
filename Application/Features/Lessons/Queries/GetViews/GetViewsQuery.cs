using Application.Features.Lessons.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Lessons.Queries.GetViews
{
    public sealed record GetViewsQuery(int CourseId, int ModuleId, int ItemId) : IRequest<OneOf<IEnumerable<StudentViewsDto>, Error>>;
}
