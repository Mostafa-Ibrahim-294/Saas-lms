namespace Domain.Entites
{
    public sealed class ZoomOAuthState
    {
        public int Id { get; set; }
        public string StateToken { get; set; } = string.Empty;
        public bool IsUsed { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }

        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
    }
}
