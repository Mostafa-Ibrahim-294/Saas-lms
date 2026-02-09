using Application.Common;
using Application.Constants;
using Application.Contracts.Repositories;
using Application.Features.TenantMembers.Dtos;

namespace Application.Features.TenantMembers.Commands.UpdateMemberRole
{
    internal sealed class UpdateMemberRoleCommandHandler : IRequestHandler<UpdateMemberRoleCommand, OneOf<UpdateMemberRoleDto, Error>>
    {
        private readonly ITenantMemberRepository _tenantMemberRepository;
        private readonly ITenantRoleRepository _tenantRoleRepository;
        private readonly ICurrentUserId _currentUserId;

        public UpdateMemberRoleCommandHandler(
            ITenantMemberRepository tenantMemberRepository,
            ITenantRoleRepository tenantRoleRepository,
            ICurrentUserId currentUserId)
        {
            _tenantMemberRepository = tenantMemberRepository;
            _tenantRoleRepository = tenantRoleRepository;
            _currentUserId = currentUserId;
        }

        public async Task<OneOf<UpdateMemberRoleDto, Error>> Handle(UpdateMemberRoleCommand request, CancellationToken cancellationToken)
        {
            var member = await _tenantMemberRepository.GetMemberByIdAsync(request.MemberId, cancellationToken);
            if (member == null)
                return TenantMemberError.MemberNotFound;

            var isOwner = await _tenantMemberRepository.IsOwnerAsync(request.MemberId, cancellationToken);
            if (isOwner)
                return TenantMemberError.CannotChangeOwnerRole;

            var currentUserId = _currentUserId.GetUserId();
            if (member.UserId == currentUserId)
                return TenantMemberError.CannotChangeOwnRole;

            await _tenantMemberRepository.UpdateRoleMemberAsync(request.MemberId, request.RoleId, cancellationToken);
            return new UpdateMemberRoleDto { Message = TenantMemberConstants.UpdateRoleMemberResponse };
        }
    }
}
