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

            _logger.LogWarning("=== STEP 0 === Callback received. State: [{State}], Code starts with: [{Code}]",
                state, request.code?[..Math.Min(8, request.code?.Length ?? 0)]);

            var oauthState = await _zoomOAuthStateRepository.GetOAuthStateAsync(state, cancellationToken);

            if (oauthState is null)
            {
                _logger.LogWarning("=== FAILED === OAuthState NOT FOUND");
                return errorUrl;
            }

            _logger.LogWarning("=== STEP 1 === OAuthState found. IsUsed={IsUsed}, ExpiresAt={ExpiresAt}",
                oauthState.IsUsed, oauthState.ExpiresAt);

            if (oauthState.IsUsed)
            {
                _logger.LogWarning("=== FAILED === OAuthState already used");
                return errorUrl;
            }

            if (oauthState.ExpiresAt < DateTime.UtcNow)
            {
                _logger.LogWarning("=== FAILED === OAuthState expired");
                return errorUrl;
            }

            oauthState.IsUsed = true;
            await _zoomOAuthStateRepository.SaveAsync(cancellationToken);
            _logger.LogWarning("=== STEP 2 === IsUsed set to true and saved");

            _logger.LogWarning("=== STEP 3 === Calling ExchangeCodeToTokenAsync...");
            var zoomTokenResponse = await _zoomService.ExchangeCodeToTokenAsync(request.code, state, cancellationToken);
            if (zoomTokenResponse is null)
            {
                oauthState.IsUsed = false;
                await _zoomOAuthStateRepository.SaveAsync(cancellationToken);
                _logger.LogWarning("=== FAILED === ExchangeCode returned null - IsUsed reset to false");
                return errorUrl;
            }
            _logger.LogWarning("=== STEP 3 DONE === Token received. AccessToken length: {Len}", zoomTokenResponse.access_token?.Length);

            _logger.LogWarning("=== STEP 4 === Calling GetZoomUserInfoAsync...");
            var zoomUserInfo = await _zoomService.GetZoomUserInfoAsync(zoomTokenResponse.access_token, cancellationToken);
            if (zoomUserInfo is null)
            {
                _logger.LogWarning("=== FAILED === GetZoomUserInfo returned null");
                return errorUrl;
            }
            _logger.LogWarning("=== STEP 4 DONE === User: {Email}", zoomUserInfo.Email);

            _logger.LogWarning("=== STEP 5 === Saving integration...");
            await _zoomIntegrationRepository.SaveOrUpdateIntegrationAsync(
                oauthState.UserId, oauthState.TenantId, zoomTokenResponse, zoomUserInfo, cancellationToken);
            await _zoomIntegrationRepository.SaveAsync(cancellationToken);

            _logger.LogWarning("=== SUCCESS === Zoom connected for user {UserId}", oauthState.UserId);
            return successUrl;
        }
    }
}