using Application.Features.TenantWebsiteSettings.Dtos;

namespace Application.Features.TenantWebsiteSettings.Commands.UpdateTenantWebsiteSettings
{
    public sealed record UpdateTenantWebsiteSettingsCommand(GeneralDto? General, EmailDto? Email, NotificationsDto? Notifications,
        AppearanceDto? Appearance) : IRequest<OneOf<TenantWebsiteSettingsResponse, Error>>;
}
