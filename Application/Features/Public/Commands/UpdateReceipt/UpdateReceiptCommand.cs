using Application.Features.TenantOrders.Dtos;

namespace Application.Features.Public.Commands.UpdateReceipt
{
    public sealed record UpdateReceiptCommand(int OrderId, string PaymentProof, string? PaymentReference)
        : IRequest<OneOf<TenantOrderResponse, Error>>;
}