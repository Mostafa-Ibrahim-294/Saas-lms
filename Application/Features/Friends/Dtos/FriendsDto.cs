namespace Application.Features.Friends.Dtos
{
    public sealed class FriendsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ProfilePicture { get; set; }
        public string? Grade { get; set; }
        public int XP { get; set; }
        public int Level { get; set; }
        public int CurrentStreak { get; set; }
    }
}