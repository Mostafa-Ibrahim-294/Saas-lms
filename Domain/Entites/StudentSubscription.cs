using Domain.Enums;

namespace Domain.Entites
{
    public sealed class StudentSubscription
    {
        public int Id { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public StudentSubscriptionStatus Status { get; set; } = StudentSubscriptionStatus.Active;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
    }
}