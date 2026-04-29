using Application.Contracts.Repositories;
using Application.Features.StudentLessons.Dtos;
using Microsoft.AspNetCore.Http;

namespace Application.Features.StudentLessons.Queries.GetStudentLessonItem
{
    internal sealed class GetStudentLessonItemQueryHandler : IRequestHandler<GetStudentLessonItemQuery, OneOf<StudentLessonItemDto, Error>>
    {
        private readonly HybridCache _hybridCache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentSubscriptionRepository _studentSubscriptionRepository;
        private readonly IModuleItemRepository _moduleItemRepository;

        public GetStudentLessonItemQueryHandler(HybridCache hybridCache, IHttpContextAccessor httpContextAccessor,
            IEnrollmentRepository enrollmentRepository, IStudentSubscriptionRepository studentSubscriptionRepository,
            IModuleItemRepository moduleItemRepository)
        {
            _hybridCache = hybridCache;
            _httpContextAccessor = httpContextAccessor;
            _enrollmentRepository = enrollmentRepository;
            _studentSubscriptionRepository = studentSubscriptionRepository;
            _moduleItemRepository = moduleItemRepository;
        }
        public async Task<OneOf<StudentLessonItemDto, Error>> Handle(GetStudentLessonItemQuery request, CancellationToken cancellationToken)
        {
            var sessionId = _httpContextAccessor.HttpContext?.Request.Cookies[AuthConstants.SessionId];
            var cachedSessionKey = $"{CacheKeysConstants.SessionKey}_{sessionId}";
            var session = await _hybridCache.GetOrCreateAsync<UserSession?>(
                cachedSessionKey,
                _ => ValueTask.FromResult<UserSession?>(null),
                cancellationToken: cancellationToken
            );
            if (session is null)
                return UserErrors.Unauthorized;

            var isEnrolled = await _enrollmentRepository.StudentIsAlreadyEnrolledAsync(session.StudentId, request.CourseId, cancellationToken);
            if (!isEnrolled)
                return StudentCourseErrors.StudentNotEnrolledInCourse;

            var subscriptionIsActive = await _studentSubscriptionRepository.StudentSubscriptionIsActiveAsync(session.StudentId, request.CourseId, cancellationToken);
            if (!subscriptionIsActive)
                return StudentSubscriptionErrors.StudentSubscribedExpired;

            var moduleItemIsExist = await _moduleItemRepository.ModuleItemIsExistAsync(request.ItemId, request.CourseId, cancellationToken);
            if (!moduleItemIsExist)
                return ModuleItemErrors.ModuleItemNotFound;

            return await _moduleItemRepository.GetStudentLessonItemAsync(request.ItemId, request.CourseId, cancellationToken);
        }
    }
}