using Application.Contracts.Repositories;
using Application.Features.Zoom.Dtos;
using Domain.Enums;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Features.Zoom.Commands.Webhook
{
    internal sealed class ZoomWebhookCommandHandler : IRequestHandler<ZoomWebhookCommand>
    {
        private readonly ILiveSessionRepository _liveSessionRepository;
        private readonly ILogger<ZoomWebhookCommandHandler> _logger;

        public ZoomWebhookCommandHandler(ILiveSessionRepository liveSessionRepository,
            ILogger<ZoomWebhookCommandHandler> logger)
        {
            _liveSessionRepository = liveSessionRepository;
            _logger = logger;
        }

        public async Task Handle(ZoomWebhookCommand request, CancellationToken cancellationToken)
        {
            var payload = JsonSerializer.Deserialize<ZoomWebhookPayload>(request.Body,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (payload is null)
            {
                _logger.LogWarning("Failed to deserialize Zoom webhook payload");
                return;
            }

            var meetingId = payload.Payload.Object.Id.ToString();

            _logger.LogInformation("Zoom webhook: {Event} for meeting {MeetingId}", payload.Event, meetingId);

            var session = await _liveSessionRepository.GetByZoomMeetingIdAsync(meetingId, cancellationToken);
            if (session is null)
            {
                _logger.LogWarning("LiveSession not found for Zoom meeting {MeetingId}", meetingId);
                return;
            }

            switch (payload.Event)
            {
                case ZoomConstants.MeetingStarted:
                    session.Status = LiveSessionStatus.Ongoing;
                    session.ActualStartTime = DateTime.UtcNow;
                    session.UpdatedAt = DateTime.UtcNow;
                    _logger.LogInformation("Meeting {MeetingId} started", meetingId);
                    break;

                case ZoomConstants.MeetingEnded:
                    session.Status = LiveSessionStatus.Completed;
                    session.ActualEndTime = DateTime.UtcNow;
                    session.UpdatedAt = DateTime.UtcNow;
                    if (session.ActualStartTime.HasValue)
                        session.RecordingDuration = (int)(DateTime.UtcNow - session.ActualStartTime.Value).TotalMinutes;
                    _logger.LogInformation("Meeting {MeetingId} ended", meetingId);
                    break;

                case ZoomConstants.ParticipantJoined:
                    _logger.LogInformation("Participant joined meeting {MeetingId}: {Name}",
                        meetingId, payload.Payload.Object.Participant?.UserName);
                    break;

                case ZoomConstants.ParticipantLeft:
                    _logger.LogInformation("Participant left meeting {MeetingId}: {Name}",
                        meetingId, payload.Payload.Object.Participant?.UserName);
                    break;

                default:
                    _logger.LogInformation("Unhandled Zoom event: {Event}", payload.Event);
                    return;
            }

            await _liveSessionRepository.SaveAsync(cancellationToken);
        }
    }
}