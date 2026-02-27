using Domain.Abstractions;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entites
{
    public sealed class ModuleItem : IAuditable
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public CourseStatus Status { get; set; }
        public ModuleItemType Type { get; set; }
        public bool AllowDiscussions { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public Assignment? Assignment { get; set; }
        public Lesson? Lesson { get; set; }
        public ICollection<ModuleItemCondition> Conditions { get; set; } = [];
    }
}
