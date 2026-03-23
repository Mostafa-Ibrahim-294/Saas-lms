namespace Application.Features.Zoom.Commands.Webhook
{
    public sealed record ZoomWebhookCommand(string Body) : IRequest;
}
