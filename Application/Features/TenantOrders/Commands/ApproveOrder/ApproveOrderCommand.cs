using Application.Features.TenantOrders.Dtos;

namespace Application.Features.TenantOrders.Commands.ApproveOrder
{
    public sealed record ApproveOrderCommand(int OrderId) : IRequest<OneOf<TenantOrderResponse, Error>>;
}
