using Application.Features.TenantMembers.Dtos;

namespace Application.Features.TenantMembers.Commands.AcceptTenanInvite
{
    public sealed record AcceptTenanInviteCommand(string token) : IRequest<OneOf<AcceptTenanInviteDto, Error>>;
}
