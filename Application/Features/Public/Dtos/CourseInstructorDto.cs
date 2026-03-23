namespace Application.Features.Public.Dtos
{
    public sealed class CourseInstructorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ProfilePicture { get; set; }
        public string Bio { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
    }
}
