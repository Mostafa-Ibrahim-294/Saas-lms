using FluentValidation;

namespace Application.Features.Tenants.Commands.CreateOnboarding
{
    public class CreateOnboardingCommandValidator : AbstractValidator<CreateOnboardingCommand>
    {
        public CreateOnboardingCommandValidator()
        {
            RuleFor(x => x.SubDomain)
                .Must(s => s == s.ToLowerInvariant()).WithMessage("حروف صغيرة فقط")
                .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$").WithMessage("SubDomain must contain only lowercase letters, numbers, and hyphens, and cannot start or end with a hyphen.");
        }
    }
}
