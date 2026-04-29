using Application.Features.Discussions.Dtos;

namespace Application.Features.Discussions.Commands.DeleteDiscussionReply
{
    public sealed record DeleteDiscussionReplyCommand(int ThreadId, int ReplyId) : IRequest<OneOf<DiscussionResponse, Error>>;
}