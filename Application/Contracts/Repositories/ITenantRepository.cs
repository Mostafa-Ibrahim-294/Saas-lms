using Application.Features.Tenants.Dtos;

namespace Application.Contracts.Repositories
{
    public interface ITenantRepository
    {
        Task<bool> IsSubDomainExistsAsync(string subDomain, CancellationToken cancellationToken);
        Task<int> CreateTenantAsync(Tenant tenant, CancellationToken cancellationToken);
        Task<(int ownerRoleId, int assistantRoleId)> AddTenantRoles(int tenantId, CancellationToken cancellationToken);
        Task AddTenantMemberAsync(TenantMember member, CancellationToken cancellationToken);
        Task AssignAssistantPermissions(int assistantRoleId, CancellationToken cancellationToken);
        Task SaveAsync(CancellationToken cancellationToken);

        Task<LastTenantDto?> GetLastTenantAsync(string? subDomain, CancellationToken cancellationToken);


        Task BeginTransactionAsync(CancellationToken cancellationToken);
        Task CommitTransactionAsync(CancellationToken cancellationToken);
        Task RollbackTransactionAsync(CancellationToken cancellationToken);

        Task<int> GetTenantIdAsync(string subDomain, CancellationToken cancellationToken);
        Task<TenantUsageDto> GetTenantUsageAsync(int tenantId, CancellationToken cancellationToken);
        Task InitializeTenantUsageAsync(List<Guid> PlanFeatureId, int SubscriptionId, int TenantId);
    }
}
