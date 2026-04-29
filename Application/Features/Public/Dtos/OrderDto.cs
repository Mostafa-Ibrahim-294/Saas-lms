using Domain.Enums;

namespace Application.Features.Public.Dtos
{
    public sealed class OrderDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public OrderStatus Status { get; set; }
        public CourseDto Course { get; set; } = new();
        public decimal PricePaid { get; set; }
        public string Currency { get; set; } = string.Empty;
        public PaymentMethodType PaymentMethod { get; set; }
        public string PaymentProof { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
