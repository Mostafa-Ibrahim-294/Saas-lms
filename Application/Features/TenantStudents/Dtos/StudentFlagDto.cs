namespace Application.Features.TenantStudents.Dtos
{
    public sealed class StudentFlagDto
    {
        public bool HasActiveSubscription { get; set; }
        public bool HasExpiredSubscription { get; set; }
        public bool HasUnpaidCourses { get; set; }
    }
}
