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
            var subDomain = _httpContextAccessor.HttpContext?.Request.Cookies[AuthConstants.SubDomain];

            var oauthState = await _zoomOAuthStateRepository.GetOAuthStateAsync(request.State, cancellationToken);
            if(oauthState == null)
                return $"https://{subDomain}{ZoomConstants.FrontendRedirectUrl}?zoom_connected=false";

            var userId = oauthState.UserId;
            var tenantId = oauthState.TenantId;
            oauthState.IsUsed = true;
            await _zoomIntegrationRepository.SaveAsync(cancellationToken);

            var zoomTokenResponse = await _zoomService.ExchangeCodeToTokenAsync(request.Code, request.State, cancellationToken);
            if(zoomTokenResponse == null)
                return $"https://{subDomain}{ZoomConstants.FrontendRedirectUrl}?zoom_connected=false";

            var zoomUserInfo = await _zoomService.GetZoomUserInfoAsync(zoomTokenResponse.AccessToken, cancellationToken);
            if(zoomUserInfo == null)
                return $"https://{subDomain}{ZoomConstants.FrontendRedirectUrl}?zoom_connected=false";

            await _zoomIntegrationRepository.SaveOrUpdateIntegrationAsync(userId, tenantId, zoomTokenResponse, zoomUserInfo, cancellationToken);
            await _zoomIntegrationRepository.SaveAsync(cancellationToken);
            return $"https://{subDomain}{ZoomConstants.FrontendRedirectUrl}?zoom_connected=true";
        }
    }
}