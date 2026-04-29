namespace Application.Features.Announcements.Dtos
{
    public sealed class AllAnnouncements
    {
        public IEnumerable<AnnouncementDto>? Data { get; set; }
        public bool HasMore { get; set; }
        public int NextCursor { get; set; }
    }
}