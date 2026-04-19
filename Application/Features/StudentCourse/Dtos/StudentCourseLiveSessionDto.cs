using Domain.Enums;

namespace Application.Features.StudentCourse.Dtos
{
    public sealed class StudentCourseLiveSessionDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public int Duration { get; set; }
        public string Teacher { get; set; } = string.Empty;
        public LiveSessionStatus Status { get; set; }
        public bool Recorded { get; set; }
        public string JoinUrl { get; set; } = string.Empty;
    }
}