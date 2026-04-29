using Application.Features.StudentAssignments.Dtos;
using Domain.Enums;

namespace Application.Features.StudentAssignments.Commands.SubmitAssignment
{
    public sealed record SubmitAssignmentCommand(int CourseId, int ItemId, SubmissionType SubmissionType, string? FileId, string? Link,
        string? TextContent) : IRequest<OneOf<StudentAssignmentResponse, Error>>;
}