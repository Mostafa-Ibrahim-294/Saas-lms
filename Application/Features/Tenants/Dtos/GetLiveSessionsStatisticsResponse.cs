namespace Application.Features.Tenants.Dtos
{
    public sealed class GetLiveSessionsStatisticsResponse
    {
        public int UpcomingSessions { get; set; }
        public int TotalSessions { get; set; }
        public decimal AttendanceRate { get; set; }
        public int CompletedSessions { get; set; }
        public int OngoingSessions { get; set; }
        public int RecordingsAvailable { get; set; }
        public int TotalStudents { get; set; }
    }
}
