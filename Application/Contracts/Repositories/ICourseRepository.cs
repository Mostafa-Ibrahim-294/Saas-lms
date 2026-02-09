using Application.Features.Courses.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.Repositories
{
    public interface ICourseRepository
    {
        Task<StatisticsDto> GetCourseStatisticsAsync(string tenantSubdomain, CancellationToken cancellationToken);
        Task<AllCoursesDto> GetAllCoursesAsync(string subDomain, string? q, int? gradeId, int? subjectId, string? sortDate, string? sortStudents, string? sortCompletion,
            int? cursor, string? lastSortValue, CancellationToken cancellationToken);
        Task<IEnumerable<LookupDto>> GetAllCoursesTitlesAsync(string subDomain, CancellationToken cancellationToken);
        Task<int> CreateCourse(Course course, CancellationToken cancellationToken);
        Task<Course?> GetCourseByIdAsync(int courseId, CancellationToken cancellationToken);
        Task RemoveCourseAsync(Course course, CancellationToken cancellationToken);
        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
