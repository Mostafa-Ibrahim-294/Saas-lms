namespace Application.Features.Discussions.Dtos
{
    public sealed class DiscussionStatisticsDto
    {
        public int TotalUnreadThreads { get; set; }
        public int UnansweredThreads { get; set; }
        public double AvgResponseTime { get; set; }
        public int ThreadsLast24h { get; set; }
    }
}