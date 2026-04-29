namespace Application.Features.Discussions.Dtos
{
    public sealed class AllDiscussionsDto
    {
        public IEnumerable<DiscussionDto> Data { get; set; } = [];
        public bool HasMore { get; set; }
        public int NextCursor { get; set; }
    }
}
