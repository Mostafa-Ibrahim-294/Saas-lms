using Application.Contracts.Repositories;
using Application.Features.StudentCourse.Dtos;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Application.Features.StudentCourse.Queries.GetStudentCourseLiveSession
{
    internal sealed class GetStudentCourseLiveSessionQueryHandler : IRequestHandler<GetStudentCourseLiveSessionQuery, OneOf<StudentCourseLiveSessionDto, Error>>
    {
        private readonly HybridCache _hybridCache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ILiveSessionRepository _liveSessionRepository;
        private readonly IStudentSubscriptionRepository _studentSubscriptionRepository;

        public GetStudentCourseLiveSessionQueryHandler(HybridCache hybridCache, IHttpContextAccessor httpContextAccessor,
            IEnrollmentRepository enrollmentRepository, ILiveSessionRepository liveSessionRepository,
            IStudentSubscriptionRepository studentSubscriptionRepository)
        {
            _hybridCache = hybridCache;
            _httpContextAccessor = httpContextAccessor;
            _enrollmentRepository = enrollmentRepository;
            _liveSessionRepository = liveSessionRepository;
            _studentSubscriptionRepository = studentSubscriptionRepository;
        }
        public async Task<OneOf<StudentCourseLiveSessionDto, Error>> Handle(GetStudentCourseLiveSessionQuery request, CancellationToken cancellationToken)
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

            var subscriptionIsActive = await _studentSubscriptionRepository.StudentSubscriptionIsActiveAsync(session.StudentId, request.CourseId, cancellationToken);
            if (!subscriptionIsActive)
                return StudentSubscriptionErrors.StudentSubscribedExpired;

            var liveSessionIsExsit = await _liveSessionRepository.LiveSessionIsExsitAsync(request.SessionId, request.CourseId, cancellationToken);
            if (!liveSessionIsExsit)
                return LiveSessionErrors.SessionNotFound;

            return await _liveSessionRepository.GetStudentCourseLiveSessionAsync(request.SessionId, request.CourseId, cancellationToken);
        }
    }
}