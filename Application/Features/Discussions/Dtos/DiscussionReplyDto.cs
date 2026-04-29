namespace Application.Features.Discussions.Dtos
{
    public sealed class DiscussionReplyDto
    {
        public int Id { get; set; }
        public int ThreadId { get; set; }
        public DiscussionAuthorDto Author { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public string Body { get; set; } = string.Empty;
    }
}