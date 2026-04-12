using Application.Contracts.Repositories;
using Application.Features.TenantStudents.Dtos;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Application.Features.Students.Commands.DeclineInvite
{
    internal class DeclineInviteCommandHandler : IRequestHandler<DeclineInviteCommand, OneOf<StudentResponse, Error>>
    {
        private readonly ICourseInviteRepository _courseInviteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantRepository _tenantRepository;
        private readonly HybridCache _hybridCache;
        private readonly IStudentRepository _studentRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeclineInviteCommandHandler(ICourseInviteRepository courseInviteRepository, IHttpContextAccessor httpContextAccessor,
            ITenantRepository tenantRepository, HybridCache hybridCache, IStudentRepository studentRepository, UserManager<ApplicationUser> userManager)
        {
            _courseInviteRepository = courseInviteRepository;
            _httpContextAccessor = httpContextAccessor;
            _tenantRepository = tenantRepository;
            _hybridCache = hybridCache;
            _studentRepository = studentRepository;
            _userManager = userManager;
        }
        public async Task<OneOf<StudentResponse, Error>> Handle(DeclineInviteCommand request, CancellationToken cancellationToken)
        {
            var tenantId = await _courseInviteRepository.GetTenantIdByInviteTokenAsync(request.Token, cancellationToken);
            var subDomain = await _tenantRepository.GetSubDomainAsync(tenantId, cancellationToken);

            var isValidToken = await _courseInviteRepository.IsValidTokenAsync(request.Token, cancellationToken);
            if (!isValidToken)
                return CourseInviteErrors.InviteExpired;

            var sessionId = _httpContextAccessor.HttpContext?.Request.Cookies[AuthConstants.SessionId];
            var cachedSessionKey = $"{CacheKeysConstants.SessionKey}_{sessionId}";
            var sessionData = await _hybridCache.GetOrCreateAsync(cachedSessionKey, async entry =>
            {
                return await Task.FromResult<string?>(null);
            }, cancellationToken: cancellationToken);

            if(sessionData is null)
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

            await _courseInviteRepository.DeclineInviteAsync(request.Token, cancellationToken);
            await _tenantRepository.DecreasePlanFeatureUsageByKeyAsync(subDomain!, FeatureConstants.STUDENT_LIMIT, cancellationToken);

            return new StudentResponse { Message = MessagesConstants.CourseInviteDeclined };
        }
    }
}