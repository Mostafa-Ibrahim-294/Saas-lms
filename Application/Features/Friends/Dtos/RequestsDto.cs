namespace Application.Features.Friends.Dtos
{
    public sealed class RequestsDto
    {
        public List<FriendRequestDto> FriendRequests { get; set; } = new();
        public List<FriendRequestDto> SentRequests { get; set; } = new();
    }
}