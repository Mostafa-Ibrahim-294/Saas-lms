using FluentValidation;

namespace Application.Features.StudentAuth.Commands.VerifyOtp
{
    public sealed class VerifyOtpCommandValidator : AbstractValidator<VerifyOtpCommand>
    {
        public VerifyOtpCommandValidator()
        {
            RuleFor(x => x.OtpCode)
                .NotEmpty().WithMessage("OTP code is required.")
                .Length(6).WithMessage("OTP code must be 6 characters long.");
        }
    }
}