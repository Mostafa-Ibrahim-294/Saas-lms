namespace Application.Features.ZoomIntegration.Dtos
{
    public sealed class ZoomMeetingResponse
    {
        public long id { get; set; }
        public string host_id { get; set; } = string.Empty;
        public string host_email { get; set; } = string.Empty;
        public string topic { get; set; } = string.Empty;
        public int type { get; set; }
        public DateTime start_time { get; set; }
        public int duration { get; set; }
        public string time_zone { get; set; } = string.Empty;
        public DateTime created_at { get; set; }
        public string start_url { get; set; } = string.Empty;
        public string join_url { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
