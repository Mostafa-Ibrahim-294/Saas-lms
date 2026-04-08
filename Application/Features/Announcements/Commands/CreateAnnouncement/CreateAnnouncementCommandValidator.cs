using Domain.Enums;
using FluentValidation;

namespace Application.Features.Announcements.Commands.CreateAnnouncement
{
    public sealed class CreateAnnouncementCommandValidator : AbstractValidator<CreateAnnouncementCommand>
    {
        public CreateAnnouncementCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .MaximumLength(500)
                .WithMessage("Title must not exceed 500 characters.");

            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Content is required.")
                .MaximumLength(2000);

            RuleFor(x => x.TargetType)
                .Must(t => t == AnnouncementType.All || t == AnnouncementType.Course)
                .WithMessage("TargetType must be either 'All' or 'Course'.");

            When(x => x.TargetType == AnnouncementType.Course, () =>
            {
                RuleFor(x => x.TargetCourseIds)
                    .NotNull()
                    .WithMessage("TargetCourseIds is required when targetType is 'Course'.")
                    .Must(ids => ids != null && ids.Any())
                    .WithMessage("At least one course ID must be provided.");
            });

            When(x => x.TargetType == AnnouncementType.All, () =>
            {
                RuleFor(x => x.TargetCourseIds)
                    .Must(ids => ids == null || !ids.Any())
                    .WithMessage("TargetCourseIds must be empty when targetType is 'All'.");
            });
        }
    }
}