using Application.Features.TenantOrders.Dtos;

namespace Application.Features.TenantOrders.Commands.DeclineOrder
{
    public sealed record DeclineOrderCommand(int OrderId, string? Reason) : IRequest<OneOf<TenantOrderResponse, Error>>;
}
