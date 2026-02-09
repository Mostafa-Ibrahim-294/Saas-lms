using Application.Features.TenantMembers.Dtos;

namespace Application.Features.TenantMembers.Queries.GetMemberProfile
{
    public sealed record GetMemberProfileQuery(int MemberId) : IRequest<OneOf<MemberProfileDto, Error>>;
}
