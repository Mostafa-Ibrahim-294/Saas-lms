using Application.Common;
using Application.Constants;
using Application.Contracts.Repositories;
using Application.Features.TenantMembers.Dtos;
using Microsoft.AspNetCore.Http;

namespace Application.Features.TenantMembers.Commands.RemoveMember
{
    internal sealed class RemoveMemberCommandHandler : IRequestHandler<RemoveMemberCommand, OneOf<RemoveMemberDto, Error>>
    {
        private readonly ITenantMemberRepository _tenantMemberRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentUserId _currentUserId;

        public RemoveMemberCommandHandler(
            ITenantMemberRepository tenantMemberRepository,
            ITenantRepository tenantRepository,
            IHttpContextAccessor httpContextAccessor,
            ICurrentUserId currentUserId)
        {
            _tenantMemberRepository = tenantMemberRepository;
            _tenantRepository = tenantRepository;
            _httpContextAccessor = httpContextAccessor;
            _currentUserId = currentUserId;
        }

        public async Task<OneOf<RemoveMemberDto, Error>> Handle(RemoveMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await _tenantMemberRepository.GetMemberByIdAsync(request.MemberId, cancellationToken);
            if (member == null)
                return TenantMemberError.MemberNotFound;

            var isOwner = await _tenantMemberRepository.IsOwnerAsync(request.MemberId, cancellationToken);
            if (isOwner)
                return TenantMemberError.CannotRemoveOwner;

            var currentUserId = _currentUserId.GetUserId();
            if (member.UserId == currentUserId)
                return TenantMemberError.CannotRemoveSelf;

            await _tenantMemberRepository.RemoveMemberAsync(request.MemberId, cancellationToken);
            return new RemoveMemberDto { Message = TenantMemberConstants.RemoveMemberResponse };
        }
    }
}
