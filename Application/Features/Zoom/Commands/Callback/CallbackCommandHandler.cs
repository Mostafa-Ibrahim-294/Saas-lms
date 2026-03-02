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
            // ✅ بدون UnescapeDataString لأن الـ state مش encoded أصلاً
            var state = request.state;
            var parts = state.Split('|');
            var subDomain = parts.Length > 1 ? parts[1] : "app";
            var successUrl = $"https://{subDomain}{ZoomConstants.FrontendRedirectUrl}?zoom_connected=true";
            var errorUrl = $"https://{subDomain}{ZoomConstants.FrontendRedirectUrl}?zoom_connected=false";

            _logger.LogWarning("=== STEP 0 === State: [{State}], Code: [{Code}]",
                state, request.code?[..Math.Min(8, request.code?.Length ?? 0)]);

            var oauthState = await _zoomOAuthStateRepository.TryMarkAsUsedAsync(state, cancellationToken);
            if (oauthState is null)
            {
                _logger.LogWarning("=== FAILED === State not found, expired, or already used");
                return errorUrl;
            }

            _logger.LogWarning("=== STEP 1 === State marked. UserId={UserId}, TenantId={TenantId}",
                oauthState.UserId, oauthState.TenantId);

            _logger.LogWarning("=== STEP 2 === Exchanging code...");
            var zoomTokenResponse = await _zoomService.ExchangeCodeToTokenAsync(request.code, state, cancellationToken);
            if (zoomTokenResponse is null)
            {
                _logger.LogWarning("=== FAILED === ExchangeCode returned null");
                return errorUrl;
            }
            _logger.LogWarning("=== STEP 2 DONE === Token received");

            _logger.LogWarning("=== STEP 3 === Getting user info...");
            var zoomUserInfo = await _zoomService.GetZoomUserInfoAsync(zoomTokenResponse.access_token, cancellationToken);
            if (zoomUserInfo is null)
            {
                _logger.LogWarning("=== FAILED === GetZoomUserInfo returned null");
                return errorUrl;
            }
            _logger.LogWarning("=== STEP 3 DONE === User: {Email}", zoomUserInfo.Email);

            _logger.LogWarning("=== STEP 4 === Saving integration...");
            await _zoomIntegrationRepository.SaveOrUpdateIntegrationAsync(
                oauthState.UserId, oauthState.TenantId, zoomTokenResponse, zoomUserInfo, cancellationToken);
            await _zoomIntegrationRepository.SaveAsync(cancellationToken);

            _logger.LogWarning("=== SUCCESS === Zoom connected for user {UserId}", oauthState.UserId);
            return successUrl;
        }
    }
}