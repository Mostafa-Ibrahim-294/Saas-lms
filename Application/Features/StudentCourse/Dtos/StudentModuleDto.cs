namespace Application.Features.StudentCourse.Dtos
{
    public sealed class StudentModuleDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TotalItems { get; set; }
        public int CompletedItems { get; set; }
        public bool IsCurrentModule { get; set; }
        public List<ModuleItemDto> ModuleItems { get; set; } = new();
    }
}