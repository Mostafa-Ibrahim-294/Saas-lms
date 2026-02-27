using Application.Features.TenantWebsite.Dtos;

namespace Application.Features.TenantWebsite.Queries.GetTenantWebsiteCourses
{
    public sealed record GetTenantCoursesQuery(List<int> CourseIds) : IRequest<List<TenantCourseDto>>;
}
