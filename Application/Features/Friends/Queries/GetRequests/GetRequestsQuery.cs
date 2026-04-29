using Application.Features.Friends.Dtos;

namespace Application.Features.Friends.Queries.GetRequests
{
    public sealed record GetRequestsQuery : IRequest<OneOf<RequestsDto, Error>>;
}