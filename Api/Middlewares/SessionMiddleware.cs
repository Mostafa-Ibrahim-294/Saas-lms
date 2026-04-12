using Application.Common;
using Application.Constants;
using Microsoft.Extensions.Caching.Hybrid;
using System.Security.Claims;
using System.Text.Json;

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

                var sessionData = await _cache.GetOrCreateAsync<string?>(
                    cachedSessionKey,
                    _ => ValueTask.FromResult<string?>(null)
                );

                if (!string.IsNullOrEmpty(sessionData))
                {
                    await _cache.SetAsync(
                        cachedSessionKey,
                        sessionData,
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
                            Expires = DateTimeOffset.UtcNow.AddDays(7),
                            Domain = AuthConstants.CookieDomain
                        }
                    );

                    var userSession = JsonSerializer.Deserialize<UserSession>(sessionData);
                    if (userSession is not null)
                    {
                        var claims = new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, userSession.UserId),
                            new Claim(ClaimTypes.Role, userSession.Role)
                        };
                        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "SessionScheme"));
                        context.User = principal;
                    }
                }
            }
            await _next(context);
        }
    }
}