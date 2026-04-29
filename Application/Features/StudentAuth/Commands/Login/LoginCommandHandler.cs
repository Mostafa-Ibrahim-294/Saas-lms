using Application.Helpers;
using Hangfire;
using Microsoft.AspNetCore.Http;

namespace Application.Features.StudentAuth.Commands.Login
{
    internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, OneOf<bool, Error>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HybridCache _hybridCache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailSender _emailSender;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager, HybridCache hybridCache,
            IHttpContextAccessor httpContextAccessor, IEmailSender emailSender)
        {
            _userManager = userManager;
            _hybridCache = hybridCache;
            _httpContextAccessor = httpContextAccessor;
            _emailSender = emailSender;
        }
        public async Task<OneOf<bool, Error>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return UserErrors.InvalidCredentials;

            var otpCode = await GenerateOtpHelper.GenerateOtp(request.Email, _hybridCache, _httpContextAccessor, cancellationToken);
            var emailBody = EmailConfirmationHelper.GenerateEmailBodyHelper(EmailConstants.LoginOtpTemplate, new Dictionary<string, string>
            {
                { "{{OTP_CODE}}", otpCode },
                { "{{UserName}}", user.FirstName }
            });
            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(request.Email, EmailConstants.EmailConfirmationSubject, emailBody));
            return true;
        }
    }
}