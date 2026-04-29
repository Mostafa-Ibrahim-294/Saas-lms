using Domain.Enums;

namespace Application.Features.Announcements.Dtos
{
    public sealed class AnnouncementDto
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPinned { get; set; }
        public AnnouncementType TargetType { get; set; }
        public int[]? TargetCourses { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}