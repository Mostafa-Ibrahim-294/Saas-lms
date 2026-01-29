using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entites
{
    public sealed class CourseProgress : IAuditable
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int CompletedLessons { get; set; }
        public int TotalLessons { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
