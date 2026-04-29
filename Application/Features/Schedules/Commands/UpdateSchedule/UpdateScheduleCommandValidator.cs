using FluentValidation;

namespace Application.Features.Schedules.Commands.UpdateSchedule
{
    public sealed class UpdateScheduleCommandValidator : AbstractValidator<UpdateScheduleCommand>
    {
        public UpdateScheduleCommandValidator()
        {
            RuleFor(s => s.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200);

            RuleFor(s => s.Description)
                .MaximumLength(1000)
                .When(s => s.Description != null);

            RuleFor(s => s.Start)
                .NotEmpty()
                .Must(BeUtc)
                .WithMessage("Start must be in UTC.");

            RuleFor(s => s.End)
                .NotEmpty()
                .Must(BeUtc)
                .WithMessage("End must be in UTC.");

            RuleFor(s => s)
                .Must(s => s.End > s.Start)
                .WithMessage("End must be greater than Start.");

            RuleFor(s => s.Type)
                .IsInEnum();

            RuleFor(s => s.CourseId)
                .GreaterThan(0);

            When(s => s.RepeatedEvent, () =>
            {
                RuleFor(s => s.RepeatFrequency)
                    .NotNull()
                    .IsInEnum();

                RuleFor(s => s.RepeatPeriodEnd)
                    .NotNull()
                    .GreaterThan(x => x.End)
                    .WithMessage("RepeatPeriodEnd must be after End.");
            });

            When(x => !x.RepeatedEvent, () =>
            {
                RuleFor(s => s.RepeatFrequency).Null();
                RuleFor(s => s.RepeatPeriodEnd).Null();
            });
        }

        private bool BeUtc(DateTime date)
        {
            return date.Kind == DateTimeKind.Utc;
        }
    }
}