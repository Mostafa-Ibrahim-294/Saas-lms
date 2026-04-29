namespace Application.Features.Discussions.Commands.CreateDiscussionThreadRead
{
    public sealed record CreateDiscussionThreadReadCommand(int ThreadId) : IRequest<OneOf<bool, Error>>;
}