using Domain.Enums;

namespace Domain.Entites
{
    public sealed class Module
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public CourseStatus Status { get; set; }
        public int Order { get; set; }
        public bool IsFree { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
