using FluentValidation;

namespace Application.Features.Tenants.Commands.CreateOnboarding
{
    public class CreateOnboardingCommandValidator : AbstractValidator<CreateOnboardingCommand>
    {
        public CreateOnboardingCommandValidator()
        {
            RuleFor(x => x.SubDomain)
    .Matches("^[a-zA-Z0-9]+(?:-[a-zA-Z0-9]+)*$")
    .WithMessage("SubDomain must contain only letters, numbers, and hyphens, cannot start or end with a hyphen, and cannot contain special characters.");
        }
    }
}
