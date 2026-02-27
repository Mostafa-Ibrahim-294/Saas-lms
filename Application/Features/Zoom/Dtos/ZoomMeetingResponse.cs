namespace Application.Features.ZoomIntegration.Dtos
{
    public sealed class ZoomMeetingResponse
    {
        public long Id { get; set; }
        public string HostId { get; set; } = string.Empty;
        public string HostEmail { get; set; } = string.Empty;
        public string Topic { get; set; } = string.Empty;
        public int Type { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public string Timezone { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string StartUrl { get; set; } = string.Empty;
        public string JoinUrl { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
