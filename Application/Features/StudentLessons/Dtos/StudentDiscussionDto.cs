namespace Application.Features.StudentLessons.Dtos
{
    public sealed class StudentDiscussionDto
    {
        public int Id { get; set; }
        public AuthorDto Author { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; } = string.Empty;
        public List<ReplyDto> Replies { get; set; } = new();
    }
}