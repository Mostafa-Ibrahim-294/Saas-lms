namespace Application.Features.TenantWebsiteSettings.Dtos
{
    public sealed class TenantWebsiteSettingsDto
    {
        public GeneralDto General { get; set; } = new GeneralDto();
        public EmailDto Email { get; set; } = new EmailDto();
        public NotificationsDto Notifications { get; set; } = new NotificationsDto();
        public AppearanceDto Appearance { get; set; } = new AppearanceDto();
    }
}