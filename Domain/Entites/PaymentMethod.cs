using Domain.Abstractions;
using Domain.Enums;
using System.Text.Json;

namespace Domain.Entites
{
    public sealed class PaymentMethod : IAuditable
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public Dictionary<string, JsonElement> Details { get; set; } = [];
        public PaymentMethodType Type { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
    }
}
