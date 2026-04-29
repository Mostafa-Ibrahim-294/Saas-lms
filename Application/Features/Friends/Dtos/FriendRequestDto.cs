using Domain.Enums;

namespace Application.Features.Friends.Dtos
{
    public sealed class FriendRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ProfilePicture { get; set; }
        public string? Grade { get; set; }
        public int StudentId { get; set; }
        public FriendStatus Status { get; set; }
    }
}