using Domain.Abstractions;

namespace Domain.Entites
{
    public sealed class PageBlock : IAuditable
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool Visible { get; set; }
        public Dictionary<string, object> Props { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int TenantPageId { get; set; }
        public TenantPage TenantPage { get; set; } = null!;
        public string BlockTypeId { get; set; } = string.Empty;
        public BlockType BlockType { get; set; } = null!;
    }
}
