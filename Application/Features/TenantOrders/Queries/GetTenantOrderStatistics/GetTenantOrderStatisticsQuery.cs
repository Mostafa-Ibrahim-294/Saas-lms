using Application.Features.TenantOrders.Dtos;

namespace Application.Features.TenantOrders.Queries.GetTenantOrderStatistics
{
    public sealed record GetTenantOrderStatisticsQuery : IRequest<TenantOrderStatisticsDto>;
}
