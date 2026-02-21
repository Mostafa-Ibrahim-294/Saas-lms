using Application.Features.Tenants.Commands.CreateLiveSession;
using Application.Features.Tenants.Commands.UpdateLiveSession;
using Application.Features.ZoomIntegration.Dtos;

namespace Application.Contracts.Zoom
{
    public interface IZoomService
    {
        string GetAuthorizationUrl(string state, CancellationToken cancellationToken);
        Task<ZoomTokenResponse?> ExchangeCodeToTokenAsync(string code, string state, CancellationToken cancellationToken);
        Task<ZoomUserResponse?> GetZoomUserInfoAsync(string accessToken, CancellationToken cancellationToken);
        Task<bool> RefreshZoomTokenAsync(ZoomIntegration integration, CancellationToken cancellationToken);
        Task<ZoomMeetingResponse?> CreateZoomMeetingAsync(string accessToken, CreateLiveSessionCommand request, CancellationToken cancellationToken);
        Task<bool> UpdateZoomMeetingAsync(string accessToken, string meetingId, UpdateLiveSessionCommand request, CancellationToken cancellationToken);
        Task<bool> DeleteZoomMeetingAsync(string accessToken, string meetingId, CancellationToken cancellationToken);
    }
}
