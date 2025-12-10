using Application.Contracts.Repositories;
using Domain.Constants;
using Domain.Entites;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    internal sealed class TenantRepository : ITenantRepository
    {
        private readonly AppDbContext _dbContext;
        private IDbContextTransaction? _transaction;

        public TenantRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddTenantMemberAsync(TenantMember tenantMember, CancellationToken cancellationToken)
        {
            await _dbContext.TenantMembers.AddAsync(tenantMember, cancellationToken);
        }

        public async Task AddTenantRoles(int tenantId, CancellationToken cancellationToken)
        {
            var roles = new List<TenantRole>
            {
                new TenantRole { Name = RolesConstants.Owner, TenantId = tenantId },
                new TenantRole { Name = RolesConstants.Assistant, TenantId = tenantId }
            };
            await _dbContext.TenantRoles.AddRangeAsync(roles, cancellationToken);
        }

        public async Task<int> CreateTenantAsync(Tenant tenant, CancellationToken cancellationToken)
        {
           await _dbContext.Tenants.AddAsync(tenant, cancellationToken);
           await SaveAsync(cancellationToken);
            return tenant.Id;
        }

        public async Task<TenantRole?> FindTenantRoleByTenantId(int tenantId, string Name, CancellationToken cancellationToken)
        {
           return await _dbContext.TenantRoles
                .FirstOrDefaultAsync(tr => tr.TenantId == tenantId && tr.Name == Name, cancellationToken);
        }

        public async Task<bool> IsSubDomainExistsAsync(string subDomain, CancellationToken cancellationToken)
        {
            return await _dbContext.Tenants
                .AnyAsync(t => t.SubDomain == subDomain, cancellationToken);
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            if (_transaction is null)
            {
                _transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            }
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            if (_transaction is null) return;

            await _dbContext.SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            if (_transaction is null) return;

            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}
