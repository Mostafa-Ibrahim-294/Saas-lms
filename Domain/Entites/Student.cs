using Domain.Abstractions;

namespace Domain.Entites
{
    public sealed class Student : IAuditable
    {
        public int Id { get; set; }
        public string ParentEmail { get; set; } = string.Empty;
        public string? Grade { get; set; }
        public int XP { get; set; } = 0;
        public int Level { get; set; } = 1;
        public string? Goal { get; set; }
        public string? Semester { get; set; }
        public string? InviteCode { get; set; }
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
        public LessonView? LessonView { get; set; }
        public AssignmentSubmission? AssignmentSubmission { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LevelUpdatedAt { get; set; }
        public ICollection<SessionParticipant> SessionParticipants { get; set; } = [];
        public ICollection<Order> Orders { get; set; } = [];
        public ICollection<StudentGrade> StudentGrades { get; set; } = [];
        public ICollection<StudentSubscription> StudentSubscriptions { get; set; } = [];
        public ICollection<StudentSubject> StudentSubjects { get; set; } = [];
    }
}
