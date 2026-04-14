namespace Infrastructure.Repositories
{
    internal sealed class StudentSubjectRepository : IStudentSubjectRepository
    {
        private readonly AppDbContext _context;

        public StudentSubjectRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateStudentSubjectAsync(List<StudentSubject> studentSubjects, CancellationToken cancellationToken)
        {
            await _context.StudentSubjects.AddRangeAsync(studentSubjects, cancellationToken);
        }
        public async Task<Dictionary<string, int>> GetSubjectIdsAsync(List<string> keys, CancellationToken cancellationToken)
        {
            return await _context.AvailableSubjects
                .Where(s => keys.Contains(s.Key))
                .Select(s => new { s.Key, s.Id })
                .ToDictionaryAsync(s => s.Key, s => s.Id, cancellationToken);
        }
    }
}