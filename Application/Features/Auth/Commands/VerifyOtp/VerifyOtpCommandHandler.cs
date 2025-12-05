using Application.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands.VerifyOtp
{
    internal sealed class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, OneOf<bool, Error>>
    {
        private readonly HybridCache _hybridCache;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRefreshRepository _refreshRepository;
        public VerifyOtpCommandHandler(HybridCache hybridCache, UserManager<ApplicationUser> userManager, IRefreshRepository refreshRepository)
        {
            _hybridCache = hybridCache;
            _userManager = userManager;
            _refreshRepository = refreshRepository;
        }
        public async Task<OneOf<bool, Error>> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            var cacheKey = $"otp-{request.Email}";
            var cachedOtp = await _hybridCache.GetOrCreateAsync(cacheKey, async entry =>
            {
               return await Task.FromResult<string?>(null);
            });
            if (cachedOtp is null || !string.Equals(cachedOtp, request.OtpCode, StringComparison.Ordinal))
            {
                return UserErrors.InvalidOtpCode;
            }

            await _hybridCache.RemoveAsync(cacheKey);
            var user = await _userManager.FindByEmailAsync(request.Email);
            user?.EmailConfirmed = true;
            await _refreshRepository.SaveAsync(cancellationToken);
            return true;
        }
    }
}
