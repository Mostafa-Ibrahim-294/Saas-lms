using Application.Contracts.Repositories;
using Application.Features.StudentCourse.Dtos;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Application.Features.StudentCourse.Queries.GetStudentCourseLiveSessions
{
    internal sealed class GetStudentCourseLiveSessionsQueryHandler : IRequestHandler<GetStudentCourseLiveSessionsQuery, OneOf<List<StudentCourseLiveSessionDto>, Error>>
    {
        private readonly HybridCache _hybridCache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ILiveSessionRepository _liveSessionRepository;

        public GetStudentCourseLiveSessionsQueryHandler(HybridCache hybridCache, IHttpContextAccessor httpContextAccessor,
            IEnrollmentRepository enrollmentRepository, ILiveSessionRepository liveSessionRepository)
        {
            _hybridCache = hybridCache;
            _httpContextAccessor = httpContextAccessor;
            _enrollmentRepository = enrollmentRepository;
            _liveSessionRepository = liveSessionRepository;
        }
        public async Task<OneOf<List<StudentCourseLiveSessionDto>, Error>> Handle(GetStudentCourseLiveSessionsQuery request, CancellationToken cancellationToken)
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

            var isEnrolled = await _enrollmentRepository.StudentIsAlreadyEnrolledAsync(session.StudentId, request.CourseId, cancellationToken);
            if (!isEnrolled)
                return StudentCourseErrors.StudentNotEnrolledInCourse;

            return await _liveSessionRepository.GetStudentCourseLiveSessionsAsync(request.CourseId, cancellationToken);
        }
    }
}