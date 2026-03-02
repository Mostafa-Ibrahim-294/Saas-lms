using Application.Features.Tenants.Dtos;

namespace Application.Features.Tenants.Commands.CreateLiveSession
{
    public sealed record CreateLiveSessionCommand(string Title, string? Description, int CourseId, DateTime ScheduledAt,
        int Duration, SessionSettingsDto Settings, NotificationSettingsDto Notifications) : IRequest<OneOf<CreateLiveSessionDto, Error>>;
}
