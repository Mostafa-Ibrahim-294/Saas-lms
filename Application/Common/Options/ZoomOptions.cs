namespace Infrastructure.Common.Options
{
    public class ZoomOptions
    {
        public string ClientId { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
        public string SecretToken { get; set; } = null!;
        public string RedirectUri { get; set; } = null!;
        public string WebhookUrl { get; set; } = null!;
        public string DeauthorizationUrl { get; set; } = null!;
    }
}
