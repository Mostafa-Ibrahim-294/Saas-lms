using Application.Features.Friends.Dtos;

namespace Application.Features.Friends.Commands.AcceptRequest
{
    public sealed record AcceptRequestCommand(int RequestId) : IRequest<OneOf<FriendResponse, Error>>;
}