using Domain.Enums;

namespace Infrastructure.Repositories
{
    internal sealed class StudentSubscriptionRepository : IStudentSubscriptionRepository
    {
        private readonly AppDbContext _context;
        public StudentSubscriptionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateSubscriptionAsync(StudentSubscription subscription, CancellationToken cancellationToken)
        {
            await _context.AddAsync(subscription, cancellationToken);
        }
        public async Task<bool> StudentSubscriptionIsActiveAsync(int studentId, int courseId, CancellationToken cancellationToken)
        {
            return await _context.StudentSubscriptions
                .AnyAsync(s => s.StudentId == studentId && s.CourseId == courseId && s.Status == StudentSubscriptionStatus.Active, cancellationToken);
        }
    }
}