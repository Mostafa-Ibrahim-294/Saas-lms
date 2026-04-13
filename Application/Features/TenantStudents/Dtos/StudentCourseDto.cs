using Domain.Enums;

namespace Application.Features.TenantStudents.Dtos
{
    public sealed class StudentCourseDto
    {
        public string CourseName { get; set; } = string.Empty;
        public EnrollmentType EnrollmentType { get; set; }
        public DateTime EnrolledAt { get; set; }
        public string InvitedBy { get; set; } = string.Empty;
        public SubscriptionStatus Status { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public BillingCycle? BillingCycle { get; set; }
        public PricingType PricingType { get; set; }
    }
}
