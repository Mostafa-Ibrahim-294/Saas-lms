using Application.Contracts.Repositories;
using Application.Features.Zoom.Dtos;
using Domain.Enums;
using System.Text.Json;

namespace Application.Features.Zoom.Commands.Webhook
{
    internal sealed class ZoomWebhookCommandHandler : IRequestHandler<ZoomWebhookCommand>
    {
        private readonly ILiveSessionRepository _liveSessionRepository;

        public ZoomWebhookCommandHandler(ILiveSessionRepository liveSessionRepository)
        {
            _liveSessionRepository = liveSessionRepository;
        }

        public async Task Handle(ZoomWebhookCommand request, CancellationToken cancellationToken)
        {
            var payload = JsonSerializer.Deserialize<ZoomWebhookPayload>(request.Body,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (payload is null)
                return;

            var meetingId = payload.Payload.Object.Id;
            var session = await _liveSessionRepository.GetByZoomMeetingIdAsync(meetingId, cancellationToken);
            if (session is null)
                return;

            switch (payload.Event)
            {
                case ZoomConstants.MeetingStarted:
                    session.Status = LiveSessionStatus.Ongoing;
                    session.ActualStartTime = DateTime.UtcNow;
                    break;

                case ZoomConstants.MeetingEnded:
                    session.ActualEndTime = DateTime.UtcNow;
                    session.Status = LiveSessionStatus.Completed;
                    if (session.ActualStartTime.HasValue)
                        session.RecordingDuration = (int)(session.ActualEndTime.Value - session.ActualStartTime.Value).TotalMinutes;
                    break;

                case ZoomConstants.ParticipantJoined:
                    return;

                case ZoomConstants.ParticipantLeft:
                    return;

                default:
                    return;
            }
            await _liveSessionRepository.SaveAsync(cancellationToken);
        }
    }
}