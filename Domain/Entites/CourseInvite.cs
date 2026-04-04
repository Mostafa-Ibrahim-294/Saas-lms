using Domain.Enums;

namespace Domain.Entites
{
    public sealed class CourseInvite
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = Guid.NewGuid().ToString();
        public TenantInviteStatus Status { get; set; } = TenantInviteStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddHours(24);
        public DateTime? AcceptedAt { get; set; }
        public int InvitedBy { get; set; }
        public TenantMember TenantMember { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
    }
}