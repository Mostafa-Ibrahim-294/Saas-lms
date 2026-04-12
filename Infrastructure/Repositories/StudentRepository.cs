using Application.Features.TenantStudents.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositories
{
    internal sealed class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private IDbContextTransaction? _transaction;
        public StudentRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<Student?> GetStudentAsync(int studentId, CancellationToken cancellationToken)
        {
            return _context.Students
                .AsNoTracking()
                .Include(s => s.User)
                .Include(s => s.StudentGrades)
                .Include(s => s.Enrollments)
                .FirstOrDefaultAsync(s => s.Id == studentId, cancellationToken);
        }
        public async Task<int> GetStudentIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _context.Students
                .AsNoTracking()
                .Where(s => s.UserId == userId)
                .Select(s => s.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<List<StudentDto>> GetStudentsAsync(string subDomain, CancellationToken cancellationToken, int? courseId = null)
        {
            var studentsQuery = _context.Students
                .AsNoTracking()
                .Where(s => s.Enrollments.Any(e => e.Course.Tenant.SubDomain == subDomain))
                .AsQueryable();

            if (courseId.HasValue)
                studentsQuery = studentsQuery.Where(s => s.Enrollments.Any(e => e.CourseId == courseId.Value));

            var students = await studentsQuery
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return students;
        }
        public async Task<StudentStatisticsDto> GetStudentStatisticsAsync(string subDomain, CancellationToken cancellationToken)
        {
            var studentsCount = await _context.Enrollments
                .AsNoTracking()
                .Where(e => e.Course.Tenant.SubDomain == subDomain)
                .Select(e => e.StudentId)
                .Distinct()
                .CountAsync(cancellationToken);

            var averageGrade = await _context.StudentGrades
                .AsNoTracking()
                .Where(sg => sg.Tenant.SubDomain == subDomain)
                .AverageAsync(sg => (double?)sg.Score, cancellationToken) ?? 0;

            return new StudentStatisticsDto
            {
                Students = studentsCount,
                AverageGrade = averageGrade,
                AttendanceRate = 0,
                ActiveStudents = 0
            };
        }
        public async Task<bool> DeleteStudentAsync(int studentId, int courseId, CancellationToken cancellationToken)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId, cancellationToken);

            if (enrollment is null)
                return false;

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<string> GetStudentUserIdAsync(int studentId, CancellationToken cancellationToken)
        {
            var userId = await _context.Students
                .AsNoTracking()
                .Where(s => s.Id == studentId)
                .Select(s => s.UserId)
                .FirstOrDefaultAsync(cancellationToken);
            return userId!;
        }
        public async Task CreateStudentAsync(Student student, CancellationToken cancellationToken)
        {
            await _context.Students.AddAsync(student, cancellationToken);
        }
        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            if (_transaction is null)
            {
                _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            }
        }
        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            if (_transaction is null) return;

            await _context.SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            if (_transaction is null) return;

            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}