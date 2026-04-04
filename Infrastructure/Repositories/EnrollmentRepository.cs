namespace Infrastructure.Repositories
{
    internal sealed class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly AppDbContext _context;

        public EnrollmentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateEnrollmentAsync(Enrollment enrollment, CancellationToken cancellationToken)
        {
            await _context.Enrollments.AddAsync(enrollment);
        }
        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> StudentIsAlreadyEnrolledAsync(int studentId, int courseId, CancellationToken cancellationToken)
        {
            return await _context.Enrollments
                 .AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId, cancellationToken);
        }
    }
}