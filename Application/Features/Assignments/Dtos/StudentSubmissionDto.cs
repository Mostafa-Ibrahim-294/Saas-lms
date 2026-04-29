using Domain.Enums;

namespace Application.Features.Assignments.Dtos
{
    public sealed class StudentSubmissionDto
    {
        public int Id { get; set; }
        public AssignmentStatus Status { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public int? EarnedMarks { get; set; }
        public int TotalMarks { get; set; }
        public string StudentName { get; set; } = null!;
        public string? ProfilePicture { get; set; }
        public string? Feedback { get; set; }
        public string? Link { get; set; }
        public string? Text { get; set; }
        public FileDto? File { get; set; }
    }
    public sealed class FileDto
    {
        public string FileName { get; set; } = null!;
        public string Url { get; set; } = null!;
    }
}
