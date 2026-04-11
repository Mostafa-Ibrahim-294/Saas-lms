using Application.Contracts.Repositories;
using Application.Features.TenantStudents.Dtos;
using Microsoft.AspNetCore.Http;

namespace Application.Features.TenantStudents.Commands.DeclineInvite
{
    internal class DeclineInviteCommandHandler : IRequestHandler<DeclineInviteCommand, OneOf<StudentResponse, Error>>
    {
        private readonly ICourseInviteRepository _courseInviteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantRepository _tenantRepository;

        public DeclineInviteCommandHandler(ICourseInviteRepository courseInviteRepository, IHttpContextAccessor httpContextAccessor,
            ITenantRepository tenantRepository)
        {
            _courseInviteRepository = courseInviteRepository;
            _httpContextAccessor = httpContextAccessor;
            _tenantRepository = tenantRepository;
        }
        public async Task<OneOf<StudentResponse, Error>> Handle(DeclineInviteCommand request, CancellationToken cancellationToken)
        {
            var tenantId = await _courseInviteRepository.GetTenantIdAsync(request.Token, cancellationToken);
            var subDomain = await _tenantRepository.GetSubDomainAsync(tenantId, cancellationToken);

            var isValidToken = await _courseInviteRepository.IsValidTokenAsync(request.Token, cancellationToken);
            if (!isValidToken)
                return CourseInviteErrors.InviteExpired;

            var studentEmail = _httpContextAccessor.HttpContext?.Session.GetString(AuthConstants.StudentEmail);
            if (studentEmail is null)
                return CourseInviteErrors.InviteError;

            var invitedEmail = await _courseInviteRepository.GetInvitedMemberEmailAsync(request.Token, cancellationToken);
            if (invitedEmail is null)
                return CourseInviteErrors.InviteError;

            if (!string.Equals(studentEmail, invitedEmail))
                return TenantInviteErrors.InviteError;

            await _courseInviteRepository.DeclineInviteAsync(request.Token, cancellationToken);
            await _tenantRepository.DecreasePlanFeatureUsageByKeyAsync(subDomain!, FeatureConstants.STUDENT_LIMIT, cancellationToken);

            return new StudentResponse { Message = MessagesConstants.CourseInviteDeclined };
        }
    }
}