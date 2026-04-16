using Domain.Enums;

namespace Domain.Entites
{
    public sealed class Enrollment
    {
        public int Id { get; set; }
        public EnrollmentType EnrollmentType { get; set; }
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
        public int? InvitedBy { get; set; }
        public TenantMember? TenantMember { get; set; }
        public int? CurrentModuleId { get; set; }
        public Module? Module { get; set; }
        public int? CurrentModuleItemId { get; set; }
        public ModuleItem? ModuleItem { get; set; }
    }
}
