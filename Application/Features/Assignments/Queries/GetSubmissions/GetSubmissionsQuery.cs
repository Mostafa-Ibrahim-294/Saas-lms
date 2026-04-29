using Application.Features.Assignments.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Assignments.Queries.GetSubmissions
{
    public sealed record GetSubmissionsQuery(int CourseId, int ModuleId, int ItemId) : IRequest<OneOf<IEnumerable<StudentSubmissionDto>, Error>>;
}
