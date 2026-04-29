using Application.Features.Assignments.Dtos;

namespace Application.Features.Assignments.Queries
{
    public sealed record GetSubmissionsQuery(int CourseId, int ModuleId, int ItemId) : IRequest<OneOf<IEnumerable<StudentSubmissionDto>, Error>>;
}
