using Application.Features.TenantMembers.Dtos;
namespace Application.Features.TenantMembers.Commands.RemoveMember
{
    public sealed record RemoveMemberCommand(int MemberId) : IRequest<OneOf<RemoveMemberDto, Error>>;
}
