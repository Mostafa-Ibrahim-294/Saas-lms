using Application.Features.TenantMembers.Dtos;

namespace Application.Contracts.Repositories
{
    public interface ITenantInviteRepository
    {
        Task CreateTenantInviteAsync(TenantInvite tenantInvite, CancellationToken cancellationToken);
        Task<int> SaveAsync(CancellationToken cancellationToken);
        Task<ValidateTenanInviteDto> GetValidateTenanInviteAsync(string token, CancellationToken cancellationToken);
    }
}
