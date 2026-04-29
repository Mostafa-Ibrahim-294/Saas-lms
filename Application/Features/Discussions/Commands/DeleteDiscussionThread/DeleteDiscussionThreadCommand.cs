using Application.Features.Discussions.Dtos;

namespace Application.Features.Discussions.Commands.DeleteDiscussionThread
{
    public sealed record DeleteDiscussionThreadCommand(int ThreadId) : IRequest<OneOf<DiscussionResponse, Error>>;
}
