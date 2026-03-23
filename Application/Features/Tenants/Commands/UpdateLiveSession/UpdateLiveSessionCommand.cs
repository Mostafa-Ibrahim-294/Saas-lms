using Application.Features.Tenants.Dtos;

namespace Application.Features.Tenants.Commands.UpdateLiveSession
{
    public sealed record UpdateLiveSessionCommand(int SessionId, string Title, string Description, int CourseId, DateTime ScheduledAt,
        int Duration, LiveSessionSettingsDto Settings, NotificationDto Notifications) : IRequest<OneOf<UpdateLiveSessionDto, Error>>;
}
