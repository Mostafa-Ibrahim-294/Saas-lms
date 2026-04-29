using Domain.Enums;

namespace Application.Features.StudentCourse.Dtos
{
    public sealed class StudentCourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public int CompletedModules { get; set; }
        public int TotalModules { get; set; }
        public TeacherDto Teacher { get; set; } = new();
        public CurrentModuleItemDto CurrentModuleItem { get; set; } = new();
        public StudentSubscriptionStatus SubscriptionStatus { get; set; }
        public DateOnly? RenewDate { get; set; }
    }
}