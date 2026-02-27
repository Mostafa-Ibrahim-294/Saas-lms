using Application.Features.ZoomIntegration.Dtos;

namespace Infrastructure.Repositories
{
    public sealed class ZoomIntegrationRepository : IZoomIntegrationRepository
    {
        private readonly AppDbContext _context;

        public ZoomIntegrationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(ZoomIntegration zoomIntegration, CancellationToken cancellationToken)
        {
            await _context.ZoomIntegrations.AddAsync(zoomIntegration, cancellationToken);
        }
        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task SaveOrUpdateIntegrationAsync(string userId, int tenantId, ZoomTokenResponse zoomToken,
            ZoomUserResponse zoomUser, CancellationToken cancellationToken)
        {
            var existingIntegration = await _context.ZoomIntegrations
                .FirstOrDefaultAsync(x => x.UserId == userId && x.TenantId == tenantId, cancellationToken);

            if (existingIntegration != null)
            {
                existingIntegration.AccessToken = zoomToken.AccessToken;
                existingIntegration.RefreshToken = zoomToken.RefreshToken;
                existingIntegration.TokenExpiresAt = DateTime.UtcNow.AddSeconds(zoomToken.ExpiresIn);
                existingIntegration.ZoomUserId = zoomUser.Id;
                existingIntegration.ZoomAccountId = zoomUser.AccountId;
                existingIntegration.ZoomEmail = zoomUser.Email;
                existingIntegration.ZoomDisplayName = $"{zoomUser.FirsName} {zoomUser.LastName}";
                existingIntegration.ZoomAccountType = zoomUser.Type.ToString();
                existingIntegration.IsActive = true;
                existingIntegration.UpdatedAt = DateTime.UtcNow;
                existingIntegration.LastSyncAt = DateTime.UtcNow;
            }
            else
            {
                var newIntegration = new ZoomIntegration
                {
                    UserId = userId,
                    TenantId = tenantId,
                    AccessToken = zoomToken.AccessToken,
                    RefreshToken = zoomToken.RefreshToken,
                    TokenExpiresAt = DateTime.UtcNow.AddSeconds(zoomToken.ExpiresIn),
                    ZoomUserId = zoomUser.Id,
                    ZoomAccountId = zoomUser.AccountId,
                    ZoomEmail = zoomUser.Email,
                    ZoomDisplayName = $"{zoomUser.FirsName} {zoomUser.LastName}",
                    ZoomAccountType = zoomUser.Type.ToString(),
                    IsActive = true,
                    LastSyncAt = DateTime.UtcNow
                };
                await _context.ZoomIntegrations.AddAsync(newIntegration);
            }
        }
        public async Task<ZoomIntegration?> GetZoomIntegrationAsync(string userId, int tenantId, CancellationToken cancellationToken)
        {
            return await _context.ZoomIntegrations
                .FirstOrDefaultAsync(zi => zi.UserId == userId && zi.TenantId == tenantId && zi.IsActive, cancellationToken);
        }
    }
}