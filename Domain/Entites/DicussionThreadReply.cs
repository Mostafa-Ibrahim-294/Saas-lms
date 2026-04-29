using Domain.Abstractions;

namespace Domain.Entites
{
    public sealed class DicussionThreadReply : IAuditable
    {
        public int Id { get; set; }
        public string Body { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public string AuthorId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
        public int DicussionId { get; set; }
        public DicussionThread DicussionThread { get; set; } = null!;
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
    }
}