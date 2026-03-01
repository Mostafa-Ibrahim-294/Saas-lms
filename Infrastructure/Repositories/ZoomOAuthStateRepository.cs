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
        public async Task<ZoomOAuthState?> TryMarkAsUsedAsync(string state, CancellationToken cancellationToken)
        {
            var updated = await _context.ZoomOAuthStates
                .Where(x => x.StateToken == state && !x.IsUsed && x.ExpiresAt > DateTime.UtcNow)
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.IsUsed, true), cancellationToken);

            if (updated == 0)
                return null;

            return await _context.ZoomOAuthStates
                .FirstOrDefaultAsync(x => x.StateToken == state, cancellationToken);
        }
        public async Task DeleteOldStatesAsync(string userId, int tenantId, CancellationToken cancellationToken)
        {
            await _context.ZoomOAuthStates
                .Where(x =>x.UserId == userId && x.TenantId == tenantId && (x.IsUsed || x.ExpiresAt < DateTime.UtcNow))
                .ExecuteDeleteAsync(cancellationToken);
        }
    }
}