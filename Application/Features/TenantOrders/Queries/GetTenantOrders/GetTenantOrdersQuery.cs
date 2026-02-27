using Application.Features.TenantOrders.Dtos;

namespace Application.Features.TenantOrders.Queries.GetTenantOrders
{
    public sealed record GetTenantOrdersQuery : IRequest<List<TenantOrderDto>>;
}
