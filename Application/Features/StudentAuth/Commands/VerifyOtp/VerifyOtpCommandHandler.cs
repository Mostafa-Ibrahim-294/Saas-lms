using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Application.Features.StudentAuth.Commands.VerifyOtp
{
    internal sealed class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, OneOf<bool, Error>>
    {
        private readonly HybridCache _hybridCache;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VerifyOtpCommandHandler(HybridCache hybridCache, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _hybridCache = hybridCache;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<OneOf<bool, Error>> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            var verificationCode = _httpContextAccessor.HttpContext?.Request.Cookies[AuthConstants.VerificationCode];
            if (verificationCode == null)
                return UserErrors.InvalidVerificationToken;

            var email = await _hybridCache.GetOrCreateAsync<string?>(
                verificationCode,
                _ => ValueTask.FromResult<string?>(null),
                cancellationToken: cancellationToken
            );
            if (email is null)
                return UserErrors.InvalidVerificationToken;

            var cachedOtp = await _hybridCache.GetOrCreateAsync<string?>(
               email,
               _ => ValueTask.FromResult<string?>(null),
               cancellationToken: cancellationToken
           );
            if (cachedOtp is null)
                return UserErrors.InvalidOtpCode;

            if (request.OtpCode != cachedOtp)
                return UserErrors.InvalidOtpCode;

            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return UserErrors.EmailNotFound;

            var sessionId = Guid.NewGuid().ToString();
            var session = new
            {
                userId = user.Id,
                role = RoleConstants.Student,
                createdAt = DateTime.UtcNow
            };

            await _hybridCache.SetAsync(
                $"{CacheKeysConstants.SessionKey}_{sessionId}",
                JsonSerializer.Serialize(session),
                new HybridCacheEntryOptions
                {
                    Expiration = TimeSpan.FromDays(7)
                },
                cancellationToken: cancellationToken
            );

            _httpContextAccessor?.HttpContext?.Response.Cookies.Delete(AuthConstants.VerificationCode);
            await _hybridCache.RemoveAsync(email, cancellationToken);
            await _hybridCache.RemoveAsync(verificationCode, cancellationToken);

            _httpContextAccessor?.HttpContext?.Response.Cookies.Append(
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
            return true;
        }
    }
}