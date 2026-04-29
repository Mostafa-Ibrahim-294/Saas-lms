using FluentValidation;

namespace Application.Features.StudentLessons.Commands.UpdateStudentDiscussion
{
    public sealed class UpdateStudentDiscussionCommandValidator : AbstractValidator<UpdateStudentDiscussionCommand>
    {
        public UpdateStudentDiscussionCommandValidator()
        {
            RuleFor(d => d.Content)
                .NotEmpty().WithMessage("Content is required")
                .MaximumLength(5000).WithMessage("Content must not exceed 5000 characters");
        }
    }
}