using Microsoft.AspNetCore.Http;

namespace Application.Features.Submissions.Commands.CreateSubmissionGrade
{
    internal sealed class CreateSubmissionGradeCommandHandler : IRequestHandler<CreateSubmissionGradeCommand, OneOf<bool, Error>>
    {
        private readonly ITenantMemberRepository _tenantMemberRepository;
        private readonly ICurrentUserId _currentUserId;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISubmissionRepository _submissionRepository;
        public CreateSubmissionGradeCommandHandler(ITenantMemberRepository tenantMemberRepository, ICurrentUserId currentUserId, ISubscriptionRepository subscriptionRepository, IHttpContextAccessor httpContextAccessor, ISubmissionRepository submissionRepository)
        {
            _tenantMemberRepository = tenantMemberRepository;
            _currentUserId = currentUserId;
            _subscriptionRepository = subscriptionRepository;
            _httpContextAccessor = httpContextAccessor;
            _submissionRepository = submissionRepository;
        }
        public async Task<OneOf<bool, Error>> Handle(CreateSubmissionGradeCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserId.GetUserId();
            var subdomain = _httpContextAccessor?.HttpContext?.Request.Cookies[AuthConstants.SubDomain];
            var isPermitted = await _tenantMemberRepository.IsPermittedMember(userId, PermissionConstants.MANAGE_SUBMISSIONS, cancellationToken);
            if (!isPermitted)
            {
                return MemberErrors.NotAllowed;
            }
            var isSubscribed = await _subscriptionRepository.HasActiveSubscriptionByTenantDomain(subdomain!, cancellationToken);
            if (!isSubscribed)
            {
                return TenantErrors.NotSubscribed;
            }
            var submission = await _submissionRepository.IsSubmissionFound(request.SubmissionId, request.ItemId, cancellationToken);
            if (!submission)
            {
                return SubmissionErrors.SubmissionNotFound;
            }
            await _submissionRepository.GradeSubmission(request.SubmissionId, request.Grade, request.Feedback, cancellationToken);
            return true;
        }
    }
}
