using Application.Features.Courses.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Courses.Queries.GetLookup
{
    public sealed record GetLookupQuery : IRequest<IEnumerable<LookupDto>>;
}
