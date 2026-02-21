using Application.Features.Tenants.Dtos;

namespace Application.Features.Tenants.Queries.GetLiveSession
{
    public sealed record GetLiveSessionQuery(int SessionId) : IRequest<LiveSessionDto>;
}
