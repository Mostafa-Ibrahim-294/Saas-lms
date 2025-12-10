using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.Repositories
{
    public interface ITenantRepository
    {
        Task<int> CreateTenantAsync (Tenant tenant, CancellationToken cancellationToken);
        Task<bool> IsSubDomainExistsAsync(string subDomain, CancellationToken cancellationToken);
        Task SaveAsync(CancellationToken cancellationToken);
        Task AddTenantRoles(int tenantId, CancellationToken cancellationToken);
        Task<TenantRole?> FindTenantRoleByTenantId(int tenantId, string Name, CancellationToken cancellationToken);
        Task AddTenantMemberAsync(TenantMember tenantMember, CancellationToken cancellationToken);
    }
}
