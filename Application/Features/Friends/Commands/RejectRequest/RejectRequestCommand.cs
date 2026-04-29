using Application.Features.Friends.Dtos;

namespace Application.Features.Friends.Commands.RejectRequest
{
    public sealed record RejectRequestCommand(int RequestId) : IRequest<OneOf<FriendResponse, Error>>;
}