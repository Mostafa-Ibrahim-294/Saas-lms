using Application.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Hybrid;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Application.Helpers
{
    public static class GenerateOtpHelper
    {
        public static async Task<string> GenerateOtp(string email, HybridCache hybridCache,
            IHttpContextAccessor httpContextAccessor, CancellationToken cancellationToken)
        {
            var otpCode = new Random().Next(100000, 999999).ToString();
            var verificationCode = new Guid().ToString();
            await hybridCache.SetAsync(verificationCode, email, cancellationToken: cancellationToken);
            await hybridCache.SetAsync(email, otpCode, new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromMinutes(2)
            }, cancellationToken: cancellationToken);
            httpContextAccessor?.HttpContext?.Response.Cookies.Append(AuthConstants.VerificationCode, verificationCode, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true
            });
            return otpCode;
        }
    }
}
