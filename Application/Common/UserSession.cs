namespace Application.Common
{
    public sealed class UserSession
    {
        public string UserId { get; set; } = string.Empty;
        public int StudentId { get; set; }
        public string Role { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}