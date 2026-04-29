using Domain.Abstractions;
using Domain.Enums;

namespace Domain.Entites
{
    public sealed class Announcement : IAuditable
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPinned { get; set; }
        public AnnouncementType TargetType { get; set; }
        public int[]? TargetCourseIds { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int CreatedBy { get; set; }
        public TenantMember TenantMember { get; set; } = null!;
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
    }
}