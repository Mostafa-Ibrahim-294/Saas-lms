using Application.Features.Courses.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.Repositories
{
    public interface ICourseRepository
    {
        Task<StatisticsDto> GetCourseStatisticsAsync(string tenantSubdomain, CancellationToken cancellationToken);
        Task<AllCoursesDto> GetAllCoursesAsync(string subDomain, string? q, int? gradeId, int? subjectId, string? sortBy, string? sortOrder, CourseStatus? status,
            int? cursor, string? lastSortValue, CancellationToken cancellationToken);
        Task<IEnumerable<LookupDto>> GetAllCoursesTitlesAsync(string subDomain, CancellationToken cancellationToken);
        Task<int> CreateCourse(Course course, CancellationToken cancellationToken);
        Task<Course?> GetCourseByIdAsync(int courseId, string subdomain, CancellationToken cancellationToken);
        Task RemoveCourseAsync(Course course, CancellationToken cancellationToken);
        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
