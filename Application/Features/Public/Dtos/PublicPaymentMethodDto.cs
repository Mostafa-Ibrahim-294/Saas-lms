using Domain.Enums;
using System.Text.Json;

namespace Application.Features.Public.Dtos
{
    public sealed class PublicPaymentMethodDto
    {
        public int Id { get; set; }
        public PaymentMethodType Type { get; set; }
        public Dictionary<string, JsonElement> Details { get; set; } = new();
    }
}