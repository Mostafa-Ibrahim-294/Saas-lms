namespace Application.Features.TenantWebsiteSettings.Dtos
{
    public sealed class NotificationsDto
    {
        public EmailSettingsDto Email { get; set; } = new EmailSettingsDto();
    }
}