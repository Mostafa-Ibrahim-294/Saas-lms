using FluentValidation;

namespace Application.Features.StudentAuth.Commands.Signup
{
    public sealed class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(x => x.FirstName)
               .NotEmpty().WithMessage("First name is required.")
               .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^(?:\+201[0-2,5]\d{8}|01[0-2,5]\d{8})$")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber))
                .WithMessage("A valid phone number is required.");
        }
    }
}
