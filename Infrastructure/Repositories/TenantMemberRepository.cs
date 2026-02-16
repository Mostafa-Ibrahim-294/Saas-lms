using Application.Constants;
using Application.Features.TenantMembers.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;

namespace Infrastructure.Repositories
{
    internal sealed class TenantMemberRepository : ITenantMemberRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TenantMemberRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<string>?> GetAllPermissions(int tenantRoleId, CancellationToken cancellationToken)
        {
            return await _context.RolePermissions
                .AsNoTracking()
                .Where(trp => trp.TenantRoleId == tenantRoleId)
                .Select(trp => trp.PermissionId)
                .ToListAsync(cancellationToken);
        }
        public async Task<CurrentTenantMemberDto?> GetCurrentTenantMemberAsync(string userId, CancellationToken cancellationToken)
        {
            return await _context.TenantMembers
                 .AsNoTracking()
                 .ProjectTo<CurrentTenantMemberDto>(_mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync(tm => tm.UserId == userId, cancellationToken);
        }
        public Task<List<TenantMembersDto>> GetTenantMembersAsync(int tenantId, CancellationToken cancellationToken)
        {
            return _context.TenantMembers
                .AsNoTracking()
                .Where(tm => tm.TenantId == tenantId)
                .ProjectTo<TenantMembersDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsPermittedMember(string userId, string permission, CancellationToken cancellationToken)
        {
            var isPermitted = await _context.TenantMembers
                .AsNoTracking()
                .FirstOrDefaultAsync(tm => tm.UserId == userId && (tm.TenantRole.HasAllPermissions || tm.TenantRole.RolePermissions.Any(p => p.PermissionId == permission)), cancellationToken);
            return isPermitted != null;
        }
        public Task<List<int>> GetTenantIdsAsync(string userId, CancellationToken cancellationToken)
        {
            return _context.TenantMembers
                .AsNoTracking()
                .Where(tm => tm.UserId == userId)
                .Select(tm => tm.TenantId)
                .ToListAsync(cancellationToken);
        }
        public Task<int> GetMemberIdByUserIdAsync(string userId, int tenantId, CancellationToken cancellationToken)
        {
            return _context.TenantMembers
                .AsNoTracking()
                .Where(tm => tm.UserId == userId && tm.TenantId == tenantId)
                .Select(tm => tm.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public Task<TenantMember?> GetMemberByIdAsync(int memberId, CancellationToken cancellationToken)
        {
            return _context.TenantMembers
                .AsNoTracking()
                .FirstOrDefaultAsync(tm => tm.Id == memberId, cancellationToken);
        }
        public async Task<bool> IsOwnerAsync(int memberId, CancellationToken cancellationToken)
        {
            return await _context.TenantMembers
                .AsNoTracking()
                .Where(tm => tm.Id == memberId)
                .Select(tm => tm.TenantRole.Name == TenantRoleConstants.Owner)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public async Task RemoveMemberAsync(int memberId, CancellationToken cancellationToken)
        {
            await _context.TenantMembers
                .Where(tm => tm.Id == memberId)
                .ExecuteDeleteAsync(cancellationToken);
        }
        public Task UpdateRoleMemberAsync(int memberId, int roleId, CancellationToken cancellationToken)
        {
            return _context.TenantMembers
                .Where(tm => tm.Id == memberId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(tm => tm.TenantRoleId, roleId), cancellationToken);
        }
        public Task<MemberProfileDto> GetMemberProfileAsync(int memberId, CancellationToken cancellationToken)
        {
            var memberProfile = _context.TenantMembers
                .AsNoTracking()
                .Where(tm => tm.Id == memberId)
                .ProjectTo<MemberProfileDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
            return memberProfile!;
        }
    }
}
