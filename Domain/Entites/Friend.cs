using Domain.Abstractions;
using Domain.Enums;

namespace Domain.Entites
{
    public sealed class Friend : IAuditable
    {
        public int Id { get; set; }
        public int Student1Id { get; set; }
        public Student Student1 { get; set; } = null!;
        public int Student2Id { get; set; }
        public Student Student2 { get; set; } = null!;
        public int ActionStudentId { get; set; }
        public Student ActionStudent { get; set; } = null!;
        public FriendStatus Status { get; set; } = FriendStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}