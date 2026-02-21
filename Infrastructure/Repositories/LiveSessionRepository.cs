using Application.Features.Tenants.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enums;

namespace Infrastructure.Repositories
{
    public sealed class LiveSessionRepository : ILiveSessionRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LiveSessionRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(LiveSession session, CancellationToken cancellationToken)
        {
            await _context.LiveSessions.AddAsync(session, cancellationToken);
        }
        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(int SessionId, CancellationToken cancellationToken)
        {
            await _context.LiveSessions.Where(ls => ls.Id == SessionId).ExecuteDeleteAsync(cancellationToken);
        }
        public async Task<LiveSession?> GetLiveSessionAsync(int sessionId, CancellationToken cancellationToken)
        {
            return await _context.LiveSessions.FirstOrDefaultAsync(ls => ls.Id == sessionId, cancellationToken);
        }
        public async Task<List<LiveSessionDto>> GetLiveSessionsByTenantIdAsync(int tenantId, CancellationToken cancellationToken)
        {
            var session = await _context.LiveSessions
                .Include(ls => ls.ZoomIntegration)
                .Include(ls => ls.Participants)
                 .Include(ls => ls.Course)
                    .ThenInclude(c => c.Enrollments)
                .Where(ls => ls.TenantId == tenantId)
                .ProjectTo<LiveSessionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return session!;
        }
        public async Task<LiveSessionDto> GetLiveSessionBySessionIdAsync(int sessionId, int tenantId, CancellationToken cancellationToken)
        {
            var session = await _context.LiveSessions
                .Include(ls => ls.ZoomIntegration)
                .Include(ls => ls.Participants)
                 .Include(ls => ls.Course)
                    .ThenInclude(c => c.Enrollments)
                .Where(ls => ls.Id == sessionId && ls.TenantId == tenantId)
                .ProjectTo<LiveSessionDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
            return session!;
        }
        public async Task<GetLiveSessionsStatisticsResponse> GetStatisticsAsync(string userId, int tenantId, CancellationToken cancellationToken)
        {
            var sessions = await _context.LiveSessions
                .Include(s => s.Participants)
                .Include(s => s.Course)
                    .ThenInclude(c => c.Enrollments)
                .Where(s => s.TenantId == tenantId && s.Host.UserId == userId)
                .ToListAsync(cancellationToken);

            var totalSessions = sessions.Count;
            var upcomingSessions = sessions.Count(s => s.Status == LiveSessionStatus.Upcoming);
            var completedSessions = sessions.Count(s => s.Status == LiveSessionStatus.Completed);
            var ongoingSessions = sessions.Count(s => s.Status == LiveSessionStatus.Ongoing);
            var recordingsAvailable = sessions.Count(s => !string.IsNullOrEmpty(s.RecordingUrl));

            decimal attendanceRate = 0;
            if (completedSessions > 0)
            {
                var completedSessionsList = sessions.Where(s => s.Status == LiveSessionStatus.Completed).ToList();
                var totalExpectedAttendance = completedSessionsList.Sum(s => s.Course.Enrollments.Count);
                var totalActualAttendance = completedSessionsList.Sum(s => s.Participants.Count(p => p.Attended));

                if (totalExpectedAttendance > 0)
                    attendanceRate = Math.Round((decimal)totalActualAttendance / totalExpectedAttendance * 100, 2);
            }

            var totalStudents = sessions
                .SelectMany(s => s.Course.Enrollments)
                .Select(e => e.StudentId)
                .Distinct()
                .Count();

            return new GetLiveSessionsStatisticsResponse
            {
                TotalSessions = totalSessions,
                UpcomingSessions = upcomingSessions,
                CompletedSessions = completedSessions,
                OngoingSessions = ongoingSessions,
                AttendanceRate = attendanceRate,
                RecordingsAvailable = recordingsAvailable,
                TotalStudents = totalStudents
            };
        }
    }
}