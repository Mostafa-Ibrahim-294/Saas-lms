using Application.Features.Tenants.Commands.UpdateLiveSession;
using FluentValidation;

namespace Application.Features.LiveSessions.Commands.UpdateSession
{
    public class UpdateLiveSessionCommandValidator : AbstractValidator<UpdateLiveSessionCommand>
    {
        public UpdateLiveSessionCommandValidator()
        {
            RuleFor(x => x.SessionId)
                .GreaterThan(0).WithMessage("Valid Session ID is required");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(200).WithMessage("Description cannot exceed 2000 characters");

            RuleFor(x => x.ScheduledAt)
                .GreaterThan(DateTime.UtcNow).WithMessage("Scheduled time must be in the future");

            RuleFor(x => x.Duration)
                .InclusiveBetween(15, 480).WithMessage("Duration must be between 15 minutes and 8 hours");
        }
    }
}