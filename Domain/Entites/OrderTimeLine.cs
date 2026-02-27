using Domain.Abstractions;
using Domain.Enums;

namespace Domain.Entites
{
    public sealed class OrderTimeLine : IAuditable
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Actor { get; set; } = string.Empty;
        public OrderTimeLineType Type { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
    }
}
