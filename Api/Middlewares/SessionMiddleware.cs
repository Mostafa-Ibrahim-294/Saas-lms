using Application.Constants;
using Microsoft.Extensions.Caching.Hybrid;

namespace Api.Middlewares
{
    public class SessionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HybridCache _cache;
        public SessionMiddleware(RequestDelegate next, HybridCache cache)
        {
            _next = next;
            _cache = cache;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var sessionId = context.Request.Cookies[AuthConstants.SessionKey];

            if (!string.IsNullOrEmpty(sessionId))
            {
                var sessionKey = $"{CacheKeysConstants.SessionKey}_{sessionId}";

                var session = await _cache.GetOrCreateAsync<string?>(
                    sessionKey,
                    _ => ValueTask.FromResult<string?>(null)
                );

                if (!string.IsNullOrEmpty(session))
                {
                    await _cache.SetAsync(
                        sessionKey,
                        session,
                        new HybridCacheEntryOptions
                        {
                            Expiration = TimeSpan.FromDays(7)
                        }
                    );

                    context.Response.Cookies.Append(
                        AuthConstants.SessionKey,
                        sessionId,
                        new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                            Expires = DateTimeOffset.UtcNow.AddDays(7),
                            Domain = AuthConstants.CookieDomain
                        }
                    );
                }
            }
            await _next(context);
        }
    }
}