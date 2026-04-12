using Microsoft.AspNetCore.Http;

namespace Application.Features.StudentAuth.Commands.Logout
{
    internal sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, OneOf<bool, Error>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HybridCache _hybridCache;

        public LogoutCommandHandler(IHttpContextAccessor httpContextAccessor, HybridCache hybridCache)
        {
            _httpContextAccessor = httpContextAccessor;
            _hybridCache = hybridCache;
        }
        public async Task<OneOf<bool, Error>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var sessionId = _httpContextAccessor.HttpContext?.Request.Cookies[AuthConstants.SessionId];

            await _hybridCache.RemoveAsync($"{CacheKeysConstants.SessionKey}_{sessionId}", cancellationToken);
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(AuthConstants.SessionId);
            return true;
        }
    }
}