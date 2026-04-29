using Application.Features.Friends.Dtos;

namespace Application.Features.Friends.Commands.SendRequest
{
    public sealed record SendRequestCommand(string InviteCode) : IRequest<OneOf<FriendResponse, Error>>;
}