using Application.Features.StudentCourse.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Infrastructure.Repositories
{
    internal sealed class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public EnrollmentRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
        public async Task<List<string>> GetEmailsByCourseIdsAsync(int[] courseIds, CancellationToken cancellationToken)
        {
            return await _context.Enrollments
                .Where(e => courseIds.Contains(e.CourseId))
                .Select(e => e.Student.User.Email!)
                .Distinct()
                .ToListAsync(cancellationToken);
        }
        public async Task<List<string>> GetAllStudentEmailsAsync(int tenantId, CancellationToken cancellationToken)
        {
            return await _context.Enrollments
                .Where(e => e.Course.TenantId == tenantId)
                .Select(e => e.Student.User.Email!)
                .Distinct()
                .ToListAsync(cancellationToken);
        }
        public async Task<List<StudentCourseDto>> GetStudentCoursesAsync(int studentId, CancellationToken cancellationToken)
        {
            return await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .ProjectTo<StudentCourseDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}