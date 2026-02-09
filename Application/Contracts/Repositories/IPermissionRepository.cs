using Application.Features.Tenants.Dtos;

namespace Application.Contracts.Repositories
{
    public interface IPermissionRepository
    {
        Task<List<TenantPermissionDto>> GetAllPermissionsAsync(CancellationToken cancellationToken);
    }
}
