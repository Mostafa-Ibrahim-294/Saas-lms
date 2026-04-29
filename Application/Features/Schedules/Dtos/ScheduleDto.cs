using Domain.Enums;

namespace Application.Features.Schedules.Dtos
{
    public sealed class ScheduleDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool AllDay { get; set; }
        public string Color { get; set; } = string.Empty;
        public ScheduleType Type { get; set; }
        public int CourseId { get; set; }
    }
}