using Application.Helpers;
using Hangfire;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Application.Features.TenantAuth.Commands.Signup
{
    internal sealed class SignupCommandHandler : IRequestHandler<SignupCommand, OneOf<bool, Error>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly HybridCache _hybridCache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SignupCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper, IEmailSender emailSender,
            HybridCache hybridCache, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailSender = emailSender;
            _hybridCache = hybridCache;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<OneOf<bool, Error>> Handle(SignupCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser is not null)
            {
                if (existingUser.EmailConfirmed)
                    return UserErrors.UserAlreadyExists;
            }
            else
            {
                var newUser = _mapper.Map<ApplicationUser>(request);
                var createUserResult = await _userManager.CreateAsync(newUser, request.Password);
                if (!createUserResult.Succeeded)
                {
                    var error = string.Join(", ", createUserResult.Errors.Select(e => e.Description).First());
                    return new Error("UserCreationFailed", error, HttpStatusCode.BadRequest);
                }
                await _userManager.AddToRoleAsync(newUser, RoleConstants.Tenant);
            }
            var otpCode = await GenerateOtpHelper.GenerateOtp(request.Email, _hybridCache, _httpContextAccessor, cancellationToken);
            var emailBody = EmailConfirmationHelper.GenerateEmailBodyHelper(EmailConstants.OtpTemplate, new Dictionary<string, string>
            {
                { "{{OTP_CODE}}", otpCode },
                { "{{UserName}}", request.FirstName }
            });
            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(request.Email, EmailConstants.EmailConfirmationSubject, emailBody));
            return true;
        }
    }
}
