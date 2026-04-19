using Application.Contracts.Repositories;
using Application.Features.StudentCourse.Dtos;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Application.Features.StudentCourse.Queries.GetStudentCourseModules
{
    internal sealed class GetStudentCourseModulesQueryHandler : IRequestHandler<GetStudentCourseModulesQuery, OneOf<List<StudentModuleDto>, Error>>
    {
        private readonly HybridCache _hybridCache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public GetStudentCourseModulesQueryHandler(HybridCache hybridCache, IHttpContextAccessor httpContextAccessor,
            IEnrollmentRepository enrollmentRepository)
        {
            _hybridCache = hybridCache;
            _httpContextAccessor = httpContextAccessor;
            _enrollmentRepository = enrollmentRepository;
        }
        public async Task<OneOf<List<StudentModuleDto>, Error>> Handle(GetStudentCourseModulesQuery request, CancellationToken cancellationToken)
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

            var result = await _enrollmentRepository.GetStudentCourseModulesAsync(session.StudentId, request.CourseId, cancellationToken);
            if (result is null)
                return StudentCourseErrors.StudentNotEnrolledInCourse;
            return result;
        }
    }
}