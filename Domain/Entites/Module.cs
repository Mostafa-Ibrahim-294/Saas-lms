using Domain.Abstractions;
using Domain.Enums;

namespace Domain.Entites
{
    public sealed class Module : IAuditable
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public CourseStatus Status { get; set; }
        public int Order { get; set; }
        public bool IsFree { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public ICollection<ModuleItem> ModuleItems { get; set; } = [];
        public ICollection<Lesson> Lessons { get; set; } = [];
        public ICollection<Assignment> Assignments { get; set; } = [];
        public ICollection<Quiz> Quizzes { get; set; } = [];

    }
}
