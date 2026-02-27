using Application.Contracts.Repositories;
using Application.Contracts.Zoom;
using Application.Features.Tenants.Dtos;
using Application.Helpers;
using Domain.Enums;
using Hangfire;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Tenants.Commands.UpdateLiveSession
{
    internal sealed class UpdateLiveSessionCommandHandler : IRequestHandler<UpdateLiveSessionCommand, OneOf<UpdateLiveSessionDto, Error>>
    {
        private readonly ICurrentUserId _currentUserId;
        private readonly ILiveSessionRepository _liveSessionRepository;
        private readonly IZoomIntegrationRepository _zoomIntegrationRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantRepository _tenantRepository;
        private readonly ITenantMemberRepository _tenantMemberRepository;
        private readonly IEmailSender _emailSender;
        private readonly IZoomService _zoomService;

        public UpdateLiveSessionCommandHandler(ICurrentUserId currentUserId, ILiveSessionRepository liveSessionRepository,
            IZoomService zoomService, IEmailSender emailSender, IZoomIntegrationRepository zoomIntegrationRepository,
            ICourseRepository courseRepository, IHttpContextAccessor httpContextAccessor, ITenantRepository tenantRepository,
            ITenantMemberRepository tenantMemberRepository)
        {
            _currentUserId = currentUserId;
            _liveSessionRepository = liveSessionRepository;
            _zoomService = zoomService;
            _emailSender = emailSender;
            _zoomIntegrationRepository = zoomIntegrationRepository;
            _courseRepository = courseRepository;
            _httpContextAccessor = httpContextAccessor;
            _tenantRepository = tenantRepository;
            _tenantMemberRepository = tenantMemberRepository;
        }
        public async Task<OneOf<UpdateLiveSessionDto, Error>> Handle(UpdateLiveSessionCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserId.GetUserId();
            var isPermitted = await _tenantMemberRepository.IsPermittedMember(userId, PermissionConstants.EDIT_SESSIONS, cancellationToken);
            if (!isPermitted)
                return MemberErrors.NotAllowed;

            var subDomain = _httpContextAccessor.HttpContext?.Request.Cookies[AuthConstants.SubDomain];
            var tenantId = await _tenantRepository.GetTenantIdAsync(subDomain!, cancellationToken);
            var tenant = await _tenantRepository.GetLastTenantAsync(subDomain, cancellationToken);

            var course = await _courseRepository.GetCourseByIdAsync(request.CourseId, subDomain!, cancellationToken);
            if (course == null)
                return CourseErrors.CourseNotFound;

            var session = await _liveSessionRepository.GetLiveSessionAsync(request.SessionId, cancellationToken);
            if (session == null)
                return LiveSessionErrors.SessionNotFound;

            if (session.Host.UserId != userId)
                return LiveSessionErrors.CannotUpdateSession;

            if (session.Status == LiveSessionStatus.Ongoing)
                return LiveSessionErrors.CannotUpdateLiveSession;

            if (session.Status == LiveSessionStatus.Completed)
                return LiveSessionErrors.CannotUpdateEndedSession;

            var zoomIntegration = await _zoomIntegrationRepository.GetZoomIntegrationAsync(userId, tenantId, cancellationToken);
            if (zoomIntegration == null || !zoomIntegration.IsActive)
                return ZoomError.ZoomAccountNotConnected;

            if (zoomIntegration.TokenExpiresAt <= DateTime.UtcNow)
            {
                var refreshed = await _zoomService.RefreshZoomTokenAsync(zoomIntegration, cancellationToken);
                if (!refreshed)
                    return ZoomError.ZoomTokenRefreshFailed;
            }

            var updated = await _zoomService.UpdateZoomMeetingAsync(zoomIntegration.AccessToken, session.ZoomMeetingId, request, cancellationToken);
            if (!updated)
                return ZoomError.ZoomMeetingUpdateFailed;


            session.Title = request.Title;
            session.Description = request.Description;
            session.ScheduledAt = request.ScheduledAt;
            session.Duration = request.Duration;
            session.CourseId = request.CourseId;
            await _liveSessionRepository.SaveAsync(cancellationToken);

            if (request.Notification.SendEmail)
            {
                foreach (var enrollment in course.Enrollments)
                {
                    var student = enrollment.Student;
                    var emailBody = EmailConfirmationHelper.GenerateEmailBodyHelper(
                        EmailConstants.UpdateLiveSessionTemplate,
                       new Dictionary<string, string>
                       {
                           { "{{session_title}}", session.Title },
                           { "{{student_name}}", student.User.FirstName + " " + student.User.LastName },
                           { "{{session_description}}", session.Description ?? "" },
                           { "{{instructor_name}}", zoomIntegration.ZoomDisplayName },
                           { "{{course_name}}", course.Title },
                           { "{{session_time}}", session.ScheduledAt.ToString("HH:mm") },
                           { "{{session_date}}", session.ScheduledAt.ToString("yyyy-MM-dd") },
                           { "{{session_duration}}", session.Duration.ToString() },
                           { "{{zoom_meeting_id}}", session.ZoomMeetingId },
                           { "{{zoom_password}}", session.ZoomPassword ?? "" },
                           { "{{join_url}}", session.ZoomJoinUrl },
                           { "{{platform_name}}", tenant!.PlatformName }
                        });
                    BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(student.User.Email!, EmailConstants.UpdateSubject, emailBody));
                }
            }
            return new UpdateLiveSessionDto { Message = "تم تحديث بيانات الجلسة بنجاح" };
        }
    }
}
