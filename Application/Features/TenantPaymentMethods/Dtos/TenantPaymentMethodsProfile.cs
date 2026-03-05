namespace Application.Features.TenantPaymentMethods.Dtos
{
    public sealed class TenantPaymentMethodsProfile : Profile
    {
        public TenantPaymentMethodsProfile()
        {
            CreateMap<PaymentMethod, PaymentMethodDto>();
        }
    }
}
