using System.Text.Json.Serialization;

namespace Application.Features.Zoom.Dtos
{
    public sealed class ZoomWebhookPayload
    {
        [JsonPropertyName("event")]
        public string Event { get; set; } = string.Empty;

        [JsonPropertyName("event_ts")]
        public long EventTimestamp { get; set; }

        [JsonPropertyName("payload")]
        public ZoomWebhookPayloadData Payload { get; set; } = null!;
    }
}
