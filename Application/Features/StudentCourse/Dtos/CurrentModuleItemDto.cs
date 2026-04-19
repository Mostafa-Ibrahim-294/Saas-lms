using Domain.Enums;

namespace Application.Features.StudentCourse.Dtos
{
    public sealed class CurrentModuleItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Order { get; set; }
        public ModuleItemType Type { get; set; }
        public CourseStatus Status { get; set; }
        public DateTime? DueDate { get; set; }
    }
}