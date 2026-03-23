namespace Application.Features.TenantPaymentMethods.Dtos
{
    public sealed class PaymentMethodResponse
    {
        public PaymentMethodDto Data { get; set; } = new();
    }
}
