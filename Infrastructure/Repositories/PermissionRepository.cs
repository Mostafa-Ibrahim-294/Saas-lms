using Application.Features.Tenants.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Infrastructure.Repositories
{
    internal sealed class PermissionRepository : IPermissionRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PermissionRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TenantPermissionDto>> GetAllPermissionsAsync(CancellationToken cancellationToken)
        {
            return await _context.Permissions
                .AsNoTracking()
                .ProjectTo<TenantPermissionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
