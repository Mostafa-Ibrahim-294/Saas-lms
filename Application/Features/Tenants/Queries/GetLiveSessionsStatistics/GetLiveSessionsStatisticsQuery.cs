using Application.Features.Tenants.Dtos;

namespace Application.Features.Tenants.Queries.GetLiveSessionsStatistics
{
    public sealed record GetLiveSessionsStatisticsQuery : IRequest<OneOf<GetLiveSessionsStatisticsResponse, Error>>;
}
