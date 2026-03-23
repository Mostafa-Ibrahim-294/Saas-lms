using System.Text.Json.Serialization;

namespace Application.Features.Zoom.Dtos
{
    public sealed class ZoomWebhookPayloadData
    {
        [JsonPropertyName("account_id")]
        public string AccountId { get; set; } = string.Empty;

        [JsonPropertyName("object")]
        public ZoomMeetingObject Object { get; set; } = null!;
    }
}
