using Application.Features.Tenants.Dtos;

namespace Application.Contracts.Repositories
{
    public interface ILiveSessionRepository
    {
        Task<LiveSession?> GetLiveSessionAsync(int sessionId, CancellationToken cancellationToken);
        Task<List<LiveSessionDto>> GetLiveSessionsByTenantIdAsync(int tenantId, CancellationToken cancellationToken);
        Task<LiveSessionDto> GetLiveSessionBySessionIdAsync(int sessionId, int tenantId, CancellationToken cancellationToken);
        Task CreateAsync(LiveSession session, CancellationToken cancellationToken);
        Task<int> SaveAsync(CancellationToken cancellationToken);
        Task DeleteAsync(int SessionId, CancellationToken cancellationToken);
        Task<GetLiveSessionsStatisticsResponse> GetStatisticsAsync(string userId, int tenantId, CancellationToken cancellationToken);

    }
}
