using Domain.Abstractions;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entites
{
    public sealed class LessonView : IAuditable
    {
        public int Id { get; set; }
        public DateTime LastWatchedAt { get; set; } = DateTime.UtcNow;
        public ViewStatus Status { get; set; } = ViewStatus.NotStarted;
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int ModuleItemId { get; set; }
        public Lesson Lesson { get; set; } = null!;
        public int WatchedSeconds { get; set; }
        public int LastPositionSeconds { get; set; }
        public int ViewCount { get; set; }
        public string Device { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
