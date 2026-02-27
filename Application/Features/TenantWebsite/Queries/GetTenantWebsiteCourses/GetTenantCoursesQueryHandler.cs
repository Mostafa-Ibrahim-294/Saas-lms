using Application.Contracts.Repositories;
using Application.Features.TenantWebsite.Dtos;
using Application.Features.TenantWebsite.Queries.GetTenantWebsiteCourses;
using Microsoft.AspNetCore.Http;

namespace Application.Features.TenantWebsite.Queries.GetTenantCourses
{
    internal sealed class GetTenantCoursesQueryHandler : IRequestHandler<GetTenantCoursesQuery, List<TenantCourseDto>>
    {
        private readonly ITenantWebsiteRepository _tenantWebsiteRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetTenantCoursesQueryHandler(ITenantWebsiteRepository tenantWebsiteRepository, ITenantRepository tenantRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _tenantWebsiteRepository = tenantWebsiteRepository;
            _tenantRepository = tenantRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<TenantCourseDto>> Handle(GetTenantCoursesQuery request, CancellationToken cancellationToken)
        {
            var subDomain = _httpContextAccessor.HttpContext?.Request.Cookies[AuthConstants.SubDomain];
            var tenantId = await _tenantRepository.GetTenantIdAsync(subDomain!, cancellationToken);

            return await _tenantWebsiteRepository.GetTenantWebsiteCoursesAsync(tenantId, request.CourseIds, cancellationToken);
        }
    }
}