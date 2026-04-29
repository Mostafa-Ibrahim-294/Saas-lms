namespace Domain.Entites
{
    public sealed class DicussionThreadRead
    {
        public int Id { get; set; }
        public DateTime LastReadAt { get; set; } = DateTime.UtcNow;
        public int DicussionId { get; set; }
        public DicussionThread DicussionThread { get; set; } = null!;
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
    }
}