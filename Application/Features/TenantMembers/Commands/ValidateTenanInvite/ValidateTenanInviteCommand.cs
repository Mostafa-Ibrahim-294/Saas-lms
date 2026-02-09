using Application.Features.TenantMembers.Dtos;

namespace Application.Features.TenantMembers.Commands.ValidateTenanInvite
{
    public sealed record ValidateTenanInviteCommand(string Token) : IRequest<ValidateTenanInviteDto>;
}
