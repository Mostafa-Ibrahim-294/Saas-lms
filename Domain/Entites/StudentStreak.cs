namespace Domain.Entites
{
    public sealed class StudentStreak
    {
        public int Id { get; set; }
        public int CurrentStreak { get; set; } = 0;
        public int LongestStreak { get; set; } = 0;
        public DateTime? LastActivityAt { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
    }
}