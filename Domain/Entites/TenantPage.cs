using Domain.Abstractions;
using Domain.Enums;

namespace Domain.Entites
{
    public sealed class TenantPage : IAuditable
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string Url { get; set; } = string.Empty;
        public bool IsHomePage { get; set; }
        public int Views { get; set; }
        public TenantPageStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
        public ICollection<PageBlock> PageBlocks { get; set; } = [];
    }
}
