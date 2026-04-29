namespace Application.Features.Quizzes.Dtos
{
    public sealed class OverviewDto
    {
        public int TotalAttempts { get; set; }
        public double AverageScore { get; set; }
        public double HighestScore { get; set; }
        public double LowestScore { get; set; }
        public double AverageTime { get; set; }
    }
}
