using Application.Features.Public.Dtos;

namespace Application.Features.Public.Queries.GetOrder
{
    public sealed record GetOrderQuery(int OrderId) : IRequest<OneOf<OrderDto, Error>>;
}