
using Application.Helpers;
using Hangfire;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Application.Features.Auth.Commands.Signup
{
    internal sealed class SignupCommandHandler : IRequestHandler<SignupCommand, OneOf<bool, Error>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly HybridCache _hybridCache;
        public SignupCommandHandler(
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IEmailSender emailSender,
            HybridCache hybridCache)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailSender = emailSender;
            _hybridCache = hybridCache;
        }
        public async Task<OneOf<bool, Error>> Handle(SignupCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser is not null)
            {
                return UserErrors.UserAlreadyExists;
            }
            var newUser = _mapper.Map<ApplicationUser>(request);
            var createUserResult = await _userManager.CreateAsync(newUser, request.Password);
            if (!createUserResult.Succeeded)
            {
                var error = string.Join(", ", createUserResult.Errors.Select(e => e.Description).First());
                return new Error("UserCreationFailed", error, HttpStatusCode.BadRequest);
            }
            
           var otpCode = await _hybridCache.GetOrCreateAsync<string>(
           $"otp-{request.Email}",
           async token =>
           {
               var newOtp = new Random().Next(100000, 999999).ToString();
               return newOtp;
           },
           options: new HybridCacheEntryOptions
           {
               Expiration = TimeSpan.FromMinutes(2)
           },
           cancellationToken: cancellationToken);
           var emailBody = EmailConfirmationHelper.GenerateEmailBodyHelper("OtpTemplate", new Dictionary<string, string>
            {
                { "{{OTP_CODE}}", otpCode },
                { "{{UserName}}", request.FirstName }
            });
            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(request.Email, "Email Confirmation", emailBody));
            return true;
        }
    }
}
