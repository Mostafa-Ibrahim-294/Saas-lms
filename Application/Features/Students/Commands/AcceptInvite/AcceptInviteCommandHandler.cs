using Application.Contracts.Repositories;
using Application.Features.TenantStudents.Dtos;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Application.Features.Students.Commands.AcceptInvite
{
    internal sealed class AcceptInviteCommandHandler : IRequestHandler<AcceptInviteCommand, OneOf<StudentResponse, Error>>
    {
        private readonly ICourseInviteRepository _courseInviteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly HybridCache _hybridCache;
        private readonly UserManager<ApplicationUser> _userManager;

        public AcceptInviteCommandHandler(ICourseInviteRepository courseInviteRepository,IHttpContextAccessor httpContextAccessor,
            IEnrollmentRepository enrollmentRepository, HybridCache hybridCache, UserManager<ApplicationUser> userManager)
        {
            _courseInviteRepository = courseInviteRepository;
            _httpContextAccessor = httpContextAccessor;
            _enrollmentRepository = enrollmentRepository;
            _hybridCache = hybridCache;
            _userManager = userManager;
        }
        public async Task<OneOf<StudentResponse, Error>> Handle(AcceptInviteCommand request, CancellationToken cancellationToken)
        {
            var isValidToken = await _courseInviteRepository.IsValidTokenAsync(request.Token, cancellationToken);
            if (!isValidToken)
                return CourseInviteErrors.InviteExpired;

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

            var student = await _userManager.FindByIdAsync(session.UserId);
            if (student is null)
                return CourseInviteErrors.InviteError;

            var invitedEmail = await _courseInviteRepository.GetInvitedMemberEmailAsync(request.Token, cancellationToken);
            if (invitedEmail is null)
                return CourseInviteErrors.InviteError;

            if (!string.Equals(student.Email, invitedEmail))
                return TenantInviteErrors.InviteError;

            var courseId = await _courseInviteRepository.GetCourseIdByTokenAsync(request.Token, cancellationToken);
            var tenantId = await _courseInviteRepository.GetTenantIdByInviteTokenAsync(request.Token, cancellationToken);

            var newEnrollment = new Enrollment
            {
                CourseId = courseId,
                StudentId = session.StudentId,
                EnrollmentType = EnrollmentType.Invited,
                TenantId = tenantId
            };
            await _enrollmentRepository.CreateEnrollmentAsync(newEnrollment, cancellationToken);
            await _enrollmentRepository.SaveAsync(cancellationToken);
            await _courseInviteRepository.AcceptInviteAsync(request.Token, cancellationToken);
            return new StudentResponse { Message = MessagesConstants.CourseInviteAccepted };
        }
    }
}