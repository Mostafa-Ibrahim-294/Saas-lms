using Domain.Enums;

namespace Domain.Entites
{
    public sealed class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public EnrollmentType EnrollmentType { get; set; }
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
    }
}
