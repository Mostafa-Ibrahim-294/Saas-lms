using Domain.Abstractions;

namespace Domain.Entites
{
    public sealed class NotificationSetting : IAuditable
    {
        public int Id { get; set; }
        public bool OrderApproved { get; set; }
        public bool OrderSubmitted { get; set; }
        public bool OrderRejected { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
    }
}
