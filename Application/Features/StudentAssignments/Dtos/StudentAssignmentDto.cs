namespace Application.Features.StudentAssignments.Dtos
{
    public sealed class StudentAssignmentDto
    {
        public AssignmentDto Assignment { get; set; } = new();
        public AssignmentSubmissionDto? AssignmentSubmission { get; set; }
    }
}