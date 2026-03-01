using Application.Contracts.Repositories;
using Application.Contracts.Zoom;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Zoom.Commands.Callback
{
    internal sealed class CallbackCommandHandler : IRequestHandler<CallbackCommand, string>
    {
        private readonly IZoomService _zoomService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IZoomOAuthStateRepository _zoomOAuthStateRepository;
        private readonly IZoomIntegrationRepository _zoomIntegrationRepository;

        public CallbackCommandHandler(IZoomService zoomService, IHttpContextAccessor httpContextAccessor,
            IZoomOAuthStateRepository zoomOAuthStateRepository, IZoomIntegrationRepository zoomIntegrationRepository)
        {
            _zoomService = zoomService;
            _httpContextAccessor = httpContextAccessor;
            _zoomOAuthStateRepository = zoomOAuthStateRepository;
            _zoomIntegrationRepository = zoomIntegrationRepository;
        }

        public async Task<string> Handle(CallbackCommand request, CancellationToken cancellationToken)
        {
            var parts = request.state.Split('|');
            var subDomain = parts.Length > 1 ? parts[1] : "app";
            var errorUrl = $"https://{subDomain}{ZoomConstants.FrontendRedirectUrl}?zoom_connected=false";

            var oauthState = await _zoomOAuthStateRepository.GetOAuthStateAsync(request.state, cancellationToken);
            if (oauthState is null || oauthState.IsUsed || oauthState.ExpiresAt < DateTime.UtcNow)
                return errorUrl;

            oauthState.IsUsed = true;
            await _zoomOAuthStateRepository.SaveAsync(cancellationToken);

            var zoomTokenResponse = await _zoomService.ExchangeCodeToTokenAsync(request.code, request.state, cancellationToken);
            if (zoomTokenResponse is null)
                return errorUrl;

            var zoomUserInfo = await _zoomService.GetZoomUserInfoAsync(zoomTokenResponse.AccessToken, cancellationToken);
            if (zoomUserInfo is null)
                return errorUrl;

            await _zoomIntegrationRepository.SaveOrUpdateIntegrationAsync(
                oauthState.UserId, oauthState.TenantId, zoomTokenResponse, zoomUserInfo, cancellationToken);
            await _zoomIntegrationRepository.SaveAsync(cancellationToken);

            return $"https://{subDomain}{ZoomConstants.FrontendRedirectUrl}?zoom_connected=true";
        }
    }
}