using Application.Features.Tenants.Dtos;

namespace Application.Features.Tenants.Queries.GetLiveSessions
{
    public sealed record GetLiveSessionsQuery : IRequest<List<LiveSessionDto>>;
}
