namespace Domain.Entites
{
    public sealed class Level
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int LevelNumber { get; set; }
        public int RequiredXp { get; set; }
    }
}