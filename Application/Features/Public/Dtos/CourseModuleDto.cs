namespace Application.Features.Public.Dtos
{
    public sealed class CourseModuleDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int LessonsCount { get; set; }
        public int Order { get; set; }
        public bool IsFree { get; set; }
    }
}