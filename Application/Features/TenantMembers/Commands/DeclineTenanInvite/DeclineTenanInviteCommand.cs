using Application.Features.TenantMembers.Dtos;

namespace Application.Features.TenantMembers.Commands.DeclineTenanInvite
{
    public sealed record DeclineTenanInviteCommand(string token) : IRequest<DeclineTenanInviteDto>;
}
