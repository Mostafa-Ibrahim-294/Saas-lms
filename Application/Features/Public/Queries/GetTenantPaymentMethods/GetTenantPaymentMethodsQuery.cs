using Application.Features.TenantPaymentMethods.Dtos;

namespace Application.Features.Public.Queries.GetTenantPaymentMethods
{
    public sealed record GetTenantPaymentMethodsQuery(string SubDomain) : IRequest<List<PaymentMethodDto>>;
}