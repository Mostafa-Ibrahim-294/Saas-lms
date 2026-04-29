using Application.Features.Public.Dtos;

namespace Application.Features.Public.Queries.GetTenantPaymentMethods
{
    public sealed record GetTenantPaymentMethodsQuery : IRequest<OneOf<List<PublicPaymentMethodDto>, Error>>;
}