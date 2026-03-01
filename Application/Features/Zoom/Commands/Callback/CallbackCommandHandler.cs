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
            var state = Uri.UnescapeDataString(request.state);
            var parts = state.Split('|');
            var subDomain = parts.Length > 1 ? parts[1] : "app";
            var successUrl = $"https://{subDomain}{ZoomConstants.FrontendRedirectUrl}?zoom_connected=true";
            var errorUrl = $"https://{subDomain}{ZoomConstants.FrontendRedirectUrl}?zoom_connected=false";

            _logger.LogWarning("Zoom callback - State: [{State}], SubDomain: [{SubDomain}]", state, subDomain);

            var oauthState = await _zoomOAuthStateRepository.GetOAuthStateAsync(state, cancellationToken);

            if (oauthState is null)
            {
                _logger.LogWarning("OAuthState NOT FOUND");
                return errorUrl;
            }

            if (oauthState.IsUsed)
            {
                _logger.LogWarning("OAuthState already used - checking if integration exists");
                var existingIntegration = await _zoomIntegrationRepository
                    .GetZoomIntegrationAsync(oauthState.UserId, oauthState.TenantId, cancellationToken);
                return existingIntegration is not null ? successUrl : errorUrl;
            }

            if (oauthState.ExpiresAt < DateTime.UtcNow)
            {
                _logger.LogWarning("OAuthState expired");
                return errorUrl;
            }

            oauthState.IsUsed = true;
            await _zoomOAuthStateRepository.SaveAsync(cancellationToken);

            var zoomTokenResponse = await _zoomService.ExchangeCodeToTokenAsync(request.code, state, cancellationToken);
            if (zoomTokenResponse is null)
            {
                _logger.LogWarning("ExchangeCode failed");
                return errorUrl;
            }

            var zoomUserInfo = await _zoomService.GetZoomUserInfoAsync(zoomTokenResponse.access_token, cancellationToken);
            if (zoomUserInfo is null)
            {
                _logger.LogWarning("GetZoomUserInfo failed");
                return errorUrl;
            }

            await _zoomIntegrationRepository.SaveOrUpdateIntegrationAsync(oauthState.UserId, oauthState.TenantId, zoomTokenResponse, zoomUserInfo, cancellationToken);
            await _zoomIntegrationRepository.SaveAsync(cancellationToken);

            _logger.LogWarning("Zoom connected successfully for user {UserId}", oauthState.UserId);
            return successUrl;
        }
    }
}