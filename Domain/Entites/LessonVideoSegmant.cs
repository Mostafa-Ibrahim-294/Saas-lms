using Domain.Abstractions;

namespace Domain.Entites
{
    public sealed class LessonVideoSegmant : IAuditable
    {
        public int Id { get; set; }
        public int StartSecond { get; set; }
        public int EndSecond { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int LessonViewId { get; set; }
        public LessonView LessonView { get; set; } = null!;
    }
}