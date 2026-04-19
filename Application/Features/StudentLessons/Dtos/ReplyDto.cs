namespace Application.Features.StudentLessons.Dtos
{
    public sealed class ReplyDto
    {
        public int Id { get; set; }
        public AuthorDto Author { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
