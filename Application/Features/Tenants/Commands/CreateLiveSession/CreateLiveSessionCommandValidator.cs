using Application.Features.Tenants.Commands.CreateLiveSession;
using FluentValidation;

namespace Application.Features.LiveSessions.Commands.CreateSession
{
    public class CreateLiveSessionCommandValidator : AbstractValidator<CreateLiveSessionCommand>
    {
        public CreateLiveSessionCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters");

            RuleFor(x => x.CourseId)
                .GreaterThan(0).WithMessage("Valid Course ID is required");

            RuleFor(x => x.ScheduledAt)
                .GreaterThan(DateTime.UtcNow).WithMessage("Scheduled time must be in the future");

            RuleFor(x => x.Duration)
                .InclusiveBetween(15, 480).WithMessage("Duration must be between 15 minutes and 8 hours");
        }
    }
}