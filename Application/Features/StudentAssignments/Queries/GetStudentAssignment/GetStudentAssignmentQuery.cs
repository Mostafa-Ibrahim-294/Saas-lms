using Application.Features.StudentAssignments.Dtos;

namespace Application.Features.StudentAssignments.Queries.GetStudentAssignment
{
    public sealed record GetStudentAssignmentQuery(int CourseId, int ItemId) : IRequest<OneOf<StudentAssignmentDto, Error>>;
}