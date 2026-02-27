using Application.Features.Tenants.Dtos;

namespace Application.Features.Tenants.Commands.UpdateLiveSession
{
    public sealed record UpdateLiveSessionCommand(int SessionId, string Title, string Description, int CourseId, DateTime ScheduledAt,
        int Duration, SessionSettingsDto Settings, NotificationDto Notification) : IRequest<OneOf<UpdateLiveSessionDto, Error>>;
}
