using Domain.Abstractions;
namespace Domain.Entites
{
    public sealed class SessionParticipant : IAuditable
    {
        public int Id { get; set; }
        public string? ZoomParticipantId { get; set; }
        public string? ZoomParticipantUuid { get; set; }
        public string? ParticipantEmail { get; set; }
        public string? ParticipantName { get; set; }
        public string? DeviceType { get; set; }
        public bool Attended { get; set; }
        public int? Duration { get; set; }
        public int JoinCount { get; set; } = 0;
        public int? TotalDuration { get; set; }
        public decimal? AttendancePercentage { get; set; }
        public AttendanceSource Source { get; set; } = AttendanceSource.Unknown;
        public bool IsManuallyMarked { get; set; }
        public string? MarkedBy { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? JoinedAt { get; set; }
        public DateTime? LeftAt { get; set; }
        public DateTime? MarkedAt { get; set; }

        public int LiveSessionId { get; set; }
        public LiveSession LiveSession { get; set; } = null!;
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
    }
}
