using Application.Features.TenantOrders.Commands.BulkOrderAction;
using Application.Features.TenantOrders.Dtos;

namespace Application.Contracts.Repositories
{
    public interface IOrderRepository
    {
        Task<List<TenantOrderDto>> GetTenantOrdersAsync(int tenantId, CancellationToken cancellationToken);
        Task<TenantOrderStatisticsDto> GetTenantOrderStatisticsAsync(int tenantId, CancellationToken cancellationToken);
        Task<bool> ApproveOrderWithEnrollmentAsync(int orderId, int tenantId, string actor,
            Enrollment enrollment, StudentSubscription subscription, CancellationToken cancellationToken);
        Task<bool> DeclineOrderAsync(int orderId, int tenantId, string actor, string? reason, CancellationToken cancellationToken);
        Task<bool> BulkOrderActionAsync(int tenantId, string actor, BulkOrderActionCommand request, CancellationToken cancellationToken);
        Task<Order?> GetOrderAsync(int tenantId, int orderId, CancellationToken cancellationToken);
    }
}