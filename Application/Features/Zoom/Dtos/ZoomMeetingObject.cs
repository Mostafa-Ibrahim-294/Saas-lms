using System.Text.Json.Serialization;

namespace Application.Features.Zoom.Dtos
{
    public sealed class ZoomMeetingObject
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("uuid")]
        public string Uuid { get; set; } = string.Empty;

        [JsonPropertyName("host_id")]
        public string HostId { get; set; } = string.Empty;

        [JsonPropertyName("topic")]
        public string Topic { get; set; } = string.Empty;

        [JsonPropertyName("start_time")]
        public string? StartTime { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("participant")]
        public ZoomParticipantObject? Participant { get; set; }
    }
}
