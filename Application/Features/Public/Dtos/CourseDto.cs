namespace Application.Features.Public.Dtos
{
    public sealed class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
    }
}
