namespace Domain.Entites
{
    public sealed class WebsiteSetting
    {
        public int Id { get; set; }
        public string? TagLine { get; set; }
        public bool IsMaintenanceMode { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
    }
}
