using Application.Contracts.Repositories;
using Application.Features.StudentCourse.Dtos;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Application.Features.StudentCourse.Queries.GetStudentCourses
{
    internal sealed class GetStudentCoursesQueryHandler : IRequestHandler<GetStudentCoursesQuery, OneOf<List<StudentCourseDto>, Error>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HybridCache _hybridCache;
        private readonly IEnrollmentRepository _enrollmentRepository;
        public GetStudentCoursesQueryHandler(IHttpContextAccessor httpContextAccessor, HybridCache hybridCache,
            IEnrollmentRepository enrollmentRepository) 
        {
            _httpContextAccessor = httpContextAccessor;
            _hybridCache = hybridCache;
            _enrollmentRepository = enrollmentRepository;
        }
        public async Task<OneOf<List<StudentCourseDto>,Error>> Handle(GetStudentCoursesQuery request, CancellationToken cancellationToken)
        {
            var sessionId = _httpContextAccessor.HttpContext?.Request.Cookies[AuthConstants.SessionId];
            var cachedSessionKey = $"{CacheKeysConstants.SessionKey}_{sessionId}";
            var sessionData = await _hybridCache.GetOrCreateAsync(cachedSessionKey, async entry =>
            {
                return await Task.FromResult<string?>(null);
            }, cancellationToken: cancellationToken);

            if (string.IsNullOrEmpty(sessionData))
                return UserErrors.Unauthorized;

            var session = JsonSerializer.Deserialize<UserSession>(sessionData);
            if (session is null)
                return UserErrors.Unauthorized;


            var cacheKey = $"{CacheKeysConstants.StudentCoursesKey}_{session.StudentId}";
            return await _hybridCache.GetOrCreateAsync(cacheKey, async entry =>
            {
                return await _enrollmentRepository.GetStudentCoursesAsync(session.StudentId, cancellationToken);
            }, new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromHours(1)
            }, cancellationToken: cancellationToken);
        }
    }
}