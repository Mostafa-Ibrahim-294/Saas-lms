namespace Application.Features.Tenants.Dtos
{
    public sealed class NotificationDto
    {
        public bool SendEmail { get; set; }
        public int ReminderTime { get; set; }
    }
}
