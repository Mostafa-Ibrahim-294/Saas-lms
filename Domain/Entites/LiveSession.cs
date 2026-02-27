using Domain.Abstractions;
using Domain.Enums;

namespace Domain.Entites
{
    public sealed class LiveSession : IAuditable
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Duration { get; set; }
        public string ZoomMeetingId { get; set; } = string.Empty;
        public string ZoomHostId { get; set; } = string.Empty;
        public string ZoomJoinUrl { get; set; } = string.Empty;
        public string ZoomStartUrl { get; set; } = string.Empty;
        public string ZoomHostEmail { get; set; } = string.Empty;
        public string? ZoomPassword { get; set; }
        public int? RecordingDuration { get; set; }
        public string? RecordingUrl { get; set; }
        public LiveSessionStatus Status { get; set; } = LiveSessionStatus.Upcoming;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime ScheduledAt { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public DateTime? ActualEndTime { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
        public int HostMemberId { get; set; }
        public TenantMember Host { get; set; } = null!;
        public int? ZoomIntegrationId { get; set; }
        public ZoomIntegration? ZoomIntegration { get; set; }
        public ICollection<SessionParticipant> Participants { get; set; } = [];
    }
}
