using Application.Common;
using Application.Constants;
using Microsoft.Extensions.Caching.Hybrid;
using System.Security.Claims;

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
            var sessionId = context.Request.Cookies[AuthConstants.SessionId];
            if (!string.IsNullOrEmpty(sessionId))
            {
                var cachedSessionKey = $"{CacheKeysConstants.SessionKey}_{sessionId}";
                var userSession = await _cache.GetOrCreateAsync<UserSession?>(
                    cachedSessionKey,
                    _ => ValueTask.FromResult<UserSession?>(null)
                );

                if (userSession is not null)
                {
                    var remainingTime = userSession.ExpiresAt - DateTimeOffset.UtcNow;
                    if (remainingTime < TimeSpan.FromDays(4))
                    {
                        userSession.ExpiresAt = DateTimeOffset.UtcNow.AddDays(7);
                        await _cache.SetAsync(
                            cachedSessionKey,
                            userSession,
                            new HybridCacheEntryOptions
                            {
                                Expiration = TimeSpan.FromDays(7)
                            }
                        );

                        context.Response.Cookies.Append(
                            AuthConstants.SessionId,
                            sessionId,
                            new CookieOptions
                            {
                                HttpOnly = true,
                                Secure = true,
                                SameSite = SameSiteMode.Strict,
                                Expires = userSession.ExpiresAt,
                                Domain = AuthConstants.CookieDomain
                            }
                        );
                    }
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userSession.UserId),
                        new Claim(ClaimTypes.Role, userSession.Role)
                    };
                    var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "SessionScheme"));
                    context.User = principal;
                }
            }
            await _next(context);
        }
    }
}