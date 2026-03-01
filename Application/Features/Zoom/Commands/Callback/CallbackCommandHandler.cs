using Application.Contracts.Repositories;
using Application.Contracts.Zoom;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Zoom.Commands.Callback
{
    internal sealed class CallbackCommandHandler : IRequestHandler<CallbackCommand, string>
    {
        private readonly IZoomService _zoomService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IZoomOAuthStateRepository _zoomOAuthStateRepository;
        private readonly IZoomIntegrationRepository _zoomIntegrationRepository;
        private readonly ILogger<CallbackCommandHandler> _logger;

        public CallbackCommandHandler(IZoomService zoomService, IHttpContextAccessor httpContextAccessor,
            IZoomOAuthStateRepository zoomOAuthStateRepository, IZoomIntegrationRepository zoomIntegrationRepository, ILogger<CallbackCommandHandler> logger)
        {
            _zoomService = zoomService;
            _httpContextAccessor = httpContextAccessor;
            _zoomOAuthStateRepository = zoomOAuthStateRepository;
            _zoomIntegrationRepository = zoomIntegrationRepository;
            _logger = logger;
        }

        public async Task<string> Handle(CallbackCommand request, CancellationToken cancellationToken)
        {
            var parts = request.state.Split('|');
            var subDomain = parts.Length > 1 ? parts[1] : "app";
            var errorUrl = $"https://{subDomain}{ZoomConstants.FrontendRedirectUrl}?zoom_connected=false";
            
            _logger.LogWarning("Handling Zoom OAuth callback with state: {State}", request.state);

            var oauthState = await _zoomOAuthStateRepository.GetOAuthStateAsync(request.state, cancellationToken);
            if (oauthState is null || oauthState.IsUsed || oauthState.ExpiresAt < DateTime.UtcNow)
                return errorUrl;

            _logger.LogWarning("Valid OAuth state found for user {UserId} and tenant {TenantId}", oauthState.UserId, oauthState.TenantId);

            oauthState.IsUsed = true;
            await _zoomOAuthStateRepository.SaveAsync(cancellationToken);

            var zoomTokenResponse = await _zoomService.ExchangeCodeToTokenAsync(request.code, request.state, cancellationToken);
            if (zoomTokenResponse is null)
            {
                _logger.LogWarning("Zoom Token Response is null!");
                return errorUrl;
            }

            _logger.LogWarning("Zoom token exchange successful for user {UserId} and tenant {TenantId}", oauthState.UserId, oauthState.TenantId);
            _logger.LogWarning("Zoom token exchange successful for user {zoomTokenResponse} ", zoomTokenResponse);

            var zoomUserInfo = await _zoomService.GetZoomUserInfoAsync(zoomTokenResponse.AccessToken, cancellationToken);
            if (zoomUserInfo is null)
            {
                _logger.LogWarning("Zoom user info is null!");
                return errorUrl;
            }

            _logger.LogWarning("Zoom user info retrieval successful for user {UserId} and tenant {TenantId}", oauthState.UserId, oauthState.TenantId);

            await _zoomIntegrationRepository.SaveOrUpdateIntegrationAsync(
                oauthState.UserId, oauthState.TenantId, zoomTokenResponse, zoomUserInfo, cancellationToken);
            await _zoomIntegrationRepository.SaveAsync(cancellationToken);

            _logger.LogWarning("Zoom integration saved for user {UserId} and tenant {TenantId}", oauthState.UserId, oauthState.TenantId);

            return $"https://{subDomain}{ZoomConstants.FrontendRedirectUrl}?zoom_connected=true";
        }
    }
}