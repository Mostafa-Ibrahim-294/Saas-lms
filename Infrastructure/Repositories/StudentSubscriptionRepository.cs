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
    }
}
