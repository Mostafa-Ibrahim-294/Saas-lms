using System.Text.Json.Serialization;

namespace Application.Features.Zoom.Dtos
{
    public sealed class ZoomParticipantObject
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        [JsonPropertyName("user_name")]
        public string? UserName { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("join_time")]
        public string? JoinTime { get; set; }

        [JsonPropertyName("leave_time")]
        public string? LeaveTime { get; set; }

        [JsonPropertyName("duration")]
        public int? Duration { get; set; }
    }
}
