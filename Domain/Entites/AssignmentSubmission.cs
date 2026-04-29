using Domain.Enums;

namespace Domain.Entites
{
    public sealed class AssignmentSubmission
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; } = null!;
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public ICollection<File>? Files { get; set; } 
        public AssignmentStatus Status { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        public double EarnedMarks { get; set; }
        public string? Feedback { get; set; }
        public string? Link { get; set; }
        public string? Text { get; set; }
    }
}
