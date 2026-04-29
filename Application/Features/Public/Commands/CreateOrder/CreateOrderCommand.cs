using Application.Features.Public.Dtos;
using Domain.Enums;

namespace Application.Features.Public.Commands.CreateOrder
{
    public sealed record CreateOrderCommand(int CourseId, PaymentMethodType PaymentMethod, string PaymentProof,
        string? PaymentReference) : IRequest<OneOf<PublicOrderDto, Error>>;
}