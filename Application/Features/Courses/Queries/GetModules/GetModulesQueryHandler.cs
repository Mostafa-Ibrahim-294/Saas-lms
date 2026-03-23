using Application.Contracts.Repositories;
using Application.Features.Courses.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Courses.Queries.GetModules
{
    internal sealed class GetModulesQueryHandler : IRequestHandler<GetModulesQuery, OneOf<CourseModuleDto, Error>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HybridCache _hybridCache;
        public GetModulesQueryHandler(ICourseRepository courseRepository, IHttpContextAccessor httpContextAccessor, HybridCache hybridCache)
        {
            _courseRepository = courseRepository;
            _httpContextAccessor = httpContextAccessor;
            _hybridCache = hybridCache;
        }
        public async Task<OneOf<CourseModuleDto, Error>> Handle(GetModulesQuery request, CancellationToken cancellationToken)
        {
            var subdomain = _httpContextAccessor?.HttpContext?.Request.Cookies[AuthConstants.SubDomain];
            var cacheKey = $"{CacheKeysConstants.CourseModuleKey}-{request.CourseId}";
            var course = await _hybridCache.GetOrCreateAsync(
                cacheKey,
                async cacheEntry =>
                {
                    return await _courseRepository.GetCourseModuleAsync(request.CourseId, subdomain!, cancellationToken);
                },
                new HybridCacheEntryOptions
                {
                    Expiration = TimeSpan.FromMinutes(30)
                },
                tags: new[] { $"{CacheKeysConstants.AllCoursesKey}_{request.CourseId}" },
                cancellationToken: cancellationToken
            );
            if (course is null)
            {
                return CourseErrors.CourseNotFound;
            }
            return course;
        }
    }
}
