using Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    internal sealed class TenantRepository : ITenantRepository
    {
        private readonly AppDbContext _context;
        public TenantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddTenantMemberAsync(TenantMember tenantMember, CancellationToken cancellationToken)
        {
            await _context.TenantMembers.AddAsync(tenantMember, cancellationToken);
        }

        public async Task AddTenantRoles(int tenantId, CancellationToken cancellationToken)
        {
            var roles = new List<TenantRole>
            {
                new TenantRole { Name = RolesConstants.Owner, TenantId = tenantId },
                new TenantRole { Name = RolesConstants.Assistant, TenantId = tenantId }
            };
            await _context.TenantRoles.AddRangeAsync(roles, cancellationToken);
        }

        public async Task<int> CreateTenantAsync(Tenant tenant, CancellationToken cancellationToken)
        {
           await _context.Tenants.AddAsync(tenant, cancellationToken);
           await SaveAsync(cancellationToken);
            return tenant.Id;
        }

        public async Task<TenantRole?> FindTenantRoleByTenantId(int tenantId, string Name, CancellationToken cancellationToken)
        {
           return await _context.TenantRoles
                .FirstOrDefaultAsync(tr => tr.TenantId == tenantId && tr.Name == Name, cancellationToken);
        }

        public async Task<bool> IsSubDomainExistsAsync(string subDomain, CancellationToken cancellationToken)
        {
            return await _context.Tenants
                .AnyAsync(t => t.SubDomain == subDomain, cancellationToken);
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
