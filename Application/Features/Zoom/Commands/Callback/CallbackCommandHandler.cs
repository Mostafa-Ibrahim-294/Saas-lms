using Application.Contracts.Repositories;
using Application.Contracts.Zoom;
using Microsoft.Extensions.Logging;

namespace Application.Features.Zoom.Commands.Callback
{
    internal sealed class CallbackCommandHandler : IRequestHandler<CallbackCommand, string>
    {
        private readonly IZoomService _zoomService;
        private readonly IZoomOAuthStateRepository _zoomOAuthStateRepository;
        private readonly IZoomIntegrationRepository _zoomIntegrationRepository;
        private readonly ILogger<CallbackCommandHandler> _logger;

        public CallbackCommandHandler(IZoomService zoomService, IZoomOAuthStateRepository zoomOAuthStateRepository,
            IZoomIntegrationRepository zoomIntegrationRepository, ILogger<CallbackCommandHandler> logger)
        {
            _zoomService = zoomService;
            _zoomOAuthStateRepository = zoomOAuthStateRepository;
            _zoomIntegrationRepository = zoomIntegrationRepository;
            _logger = logger;
        }

        public async Task<string> Handle(CallbackCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Raw state received from Zoom: [{State}]", request.state);

            var parts = request.state.Split('|');
            var subDomain = parts.Length > 1 ? parts[1] : "app";
            var errorUrl = $"https://{subDomain}{ZoomConstants.FrontendRedirectUrl}?zoom_connected=false";

            _logger.LogWarning("SubDomain extracted: [{SubDomain}]", subDomain);

            var oauthState = await _zoomOAuthStateRepository.GetOAuthStateAsync(request.state, cancellationToken);

            _logger.LogWarning("OAuthState lookup: {Result}",
                oauthState == null ? "NOT FOUND" : $"FOUND - IsUsed={oauthState.IsUsed}, ExpiresAt={oauthState.ExpiresAt}");

            if (oauthState is null || oauthState.IsUsed || oauthState.ExpiresAt < DateTime.UtcNow)
                return errorUrl;

            oauthState.IsUsed = true;
            await _zoomOAuthStateRepository.SaveAsync(cancellationToken);

            var zoomTokenResponse = await _zoomService.ExchangeCodeToTokenAsync(request.code, request.state, cancellationToken);
            if (zoomTokenResponse is null)
            {
                _logger.LogWarning("ExchangeCode failed - Token response is null");
                return errorUrl;
            }

            var zoomUserInfo = await _zoomService.GetZoomUserInfoAsync(zoomTokenResponse.access_token, cancellationToken);
            if (zoomUserInfo is null)
            {
                _logger.LogWarning("GetZoomUserInfo failed - User info is null");
                return errorUrl;
            }

            await _zoomIntegrationRepository.SaveOrUpdateIntegrationAsync(
                oauthState.UserId, oauthState.TenantId, zoomTokenResponse, zoomUserInfo, cancellationToken);
            await _zoomIntegrationRepository.SaveAsync(cancellationToken);

            _logger.LogWarning("Zoom integration saved successfully for user {UserId}", oauthState.UserId);

            return $"https://{subDomain}{ZoomConstants.FrontendRedirectUrl}?zoom_connected=true";
        }
    }
}