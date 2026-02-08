using Application.Features.TenantMembers.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Infrastructure.Repositories
{
    internal class TenantInviteRepository : ITenantInviteRepository
    {
        private readonly AppDbContext _context;
        private readonly AutoMapper.IMapper _mapper;

        public TenantInviteRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task CreateTenantInviteAsync(TenantInvite tenantInvite, CancellationToken cancellationToken)
        {
            await _context.TenantInvites.AddAsync(tenantInvite, cancellationToken);
        }
        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<ValidateTenanInviteDto> GetValidateTenanInviteAsync(string token, CancellationToken cancellationToken)
        {
            var result = await _context.TenantInvites
                .AsNoTracking()
                .Where(ti => ti.Token == token)
                .ProjectTo<ValidateTenanInviteDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
            return result!;
        }
    }
}
