using Application.Features.Discussions.Dtos;

namespace Application.Features.Discussions.Queries.GetDiscussionReplies
{
    public sealed record GetDiscussionRepliesQuery(int ThreadId) : IRequest<OneOf<List<DiscussionReplyDto>, Error>>;
}
