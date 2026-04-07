using Application.Features.Discussions.Dtos;

namespace Application.Features.Discussions.Queries.GetDiscussionStatistics
{
    public sealed record GetDiscussionStatisticsQuery : IRequest<DiscussionStatisticsDto>;
}
