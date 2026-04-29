using Domain.Abstractions;
using Domain.Enums;

namespace Domain.Entites
{
    public sealed class Schedule : IAuditable
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool AllDay { get; set; }
        public string Color { get; set; } = string.Empty;
        public bool RepeatEvent { get; set; }
        public ScheduleType Type { get; set; }
        public ScheduleRepeatFrequency? RepeatFrequency { get; set; }
        public DateTime? RepeatPeriodEnd { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}