namespace Domain.Entites
{
    public sealed class EmailSetting
    {
        public int Id { get; set; }
        public string SenderEmail { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public string ReplyToEmail { get; set; } = string.Empty;
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
    }
}
