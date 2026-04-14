using Domain.Enums;

namespace Application.Features.StudentCourse.Dtos
{
    public sealed class StudentCourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public string[]? Tags { get; set; }
        public string Grade { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public TeacherDto Teacher { get; set; } = new();
        public int TotalStudents { get; set; }
        public int TotalLessons { get; set; }
        public int CompletedLessons { get; set; }
        public double Progress { get; set; }
        public StudentSubscriptionStatus SubscriptionStatus { get; set; }
        public DateOnly? RenewDate { get; set; }
    }
}