using Application.Features.Friends.Dtos;

namespace Application.Features.Friends.Queries.GetFriends
{
    public sealed record GetStudentFriendsQuery : IRequest<OneOf<List<FriendsDto>, Error>>;
}