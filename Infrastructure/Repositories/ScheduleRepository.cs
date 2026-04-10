namespace Infrastructure.Repositories
{
    internal sealed class ScheduleRepository : IScheduleRepository
    {
        private readonly AppDbContext _context;

        public ScheduleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateScheduleAsync(Schedule schedule, CancellationToken cancellationToken)
        {
            await _context.Schedules.AddAsync(schedule);
        }
        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> HasConflictAsync(string subDomain, DateTime start, DateTime end, bool allDay, CancellationToken cancellationToken, int? scheduleId = null)
        {
            var startOfDay = start.Date;
            var endOfDay = startOfDay.AddDays(1).AddTicks(-1);

            return await _context.Schedules
                .Where(s => s.Tenant.SubDomain == subDomain && (scheduleId == null || s.Id != scheduleId))
                .AnyAsync(s => (s.AllDay && s.StartAt.Date == start.Date) ||
                    (allDay && s.StartAt < endOfDay && s.EndAt > startOfDay) ||
                    (!s.AllDay && !allDay && s.StartAt < end && s.EndAt > start),
                cancellationToken);
        }
        public async Task<Schedule?> GetScheduleByIdAsync(int scheduleId, string subDoamin, CancellationToken cancellationToken)
        {
            return await _context.Schedules
                .FirstOrDefaultAsync(s => s.Id == scheduleId && s.Tenant.SubDomain == subDoamin, cancellationToken);
        }
        public Task UpdateScheduleAsync(Schedule schedule, CancellationToken cancellationToken)
        {
            _context.Schedules.Update(schedule);
            return Task.CompletedTask;
        }
        public async Task<bool> DeleteScheduleAsync(int scheduleId, string subDomain, CancellationToken cancellationToken)
        {
            var schedule = await _context.Schedules
                .FirstOrDefaultAsync(s => s.Id == scheduleId && s.Tenant.SubDomain == subDomain, cancellationToken);
            if (schedule == null)
                return false;
            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}