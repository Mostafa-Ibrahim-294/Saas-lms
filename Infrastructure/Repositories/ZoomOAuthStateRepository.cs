namespace Infrastructure.Repositories
{
    internal sealed class ZoomOAuthStateRepository : IZoomOAuthStateRepository
    {
        private readonly AppDbContext _context;

        public ZoomOAuthStateRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(ZoomOAuthState oauthState, CancellationToken cancellationToken)
        {
            await _context.ZoomOAuthStates.AddAsync(oauthState, cancellationToken);
        }
        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<ZoomOAuthState?> GetOAuthStateAsync(string state, CancellationToken cancellationToken) =>
            await _context.ZoomOAuthStates
                .FirstOrDefaultAsync(x => x.StateToken == state, cancellationToken);
        public async Task DeleteOldStatesAsync(string userId, int tenantId, CancellationToken cancellationToken)
        {
            var oldStates = await _context.ZoomOAuthStates
                .Where(x => x.UserId == userId && x.TenantId == tenantId)
                .ToListAsync(cancellationToken);

            if (oldStates.Any())
                _context.ZoomOAuthStates.RemoveRange(oldStates);
        }
    }
}