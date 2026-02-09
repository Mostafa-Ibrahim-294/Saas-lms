using Domain.Enums;

namespace Domain.Entites
{
    public sealed class TenantInvite
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = Guid.NewGuid().ToString();
        public TenantInviteStatus Status { get; set; } = TenantInviteStatus.Pending;
        public int InvitedBy { get; set; }
        public TenantMember TenantMember { get; set; } = null!;
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
        public int TenantRoleId { get; set; }
        public TenantRole TenantRole { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddHours(24);
        public DateTime? AcceptedAt { get; set; }
    }
}
