namespace Application.Contracts.Repositories
{
    public interface IZoomOAuthStateRepository
    {
        Task CreateAsync(ZoomOAuthState oauthState, CancellationToken cancellationToken);
        Task<ZoomOAuthState?> GetOAuthStateAsync(string state, CancellationToken cancellationToken);
        Task DeleteOldStatesAsync(string userId, int tenantId, CancellationToken cancellationToken);
        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
