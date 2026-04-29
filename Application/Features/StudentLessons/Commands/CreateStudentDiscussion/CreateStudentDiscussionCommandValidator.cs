using FluentValidation;

namespace Application.Features.StudentLessons.Commands.CreateStudentDiscussion
{
    public sealed class CreateStudentDiscussionCommandValidator : AbstractValidator<CreateStudentDiscussionCommand>
    {
        public CreateStudentDiscussionCommandValidator()
        {
            RuleFor(d => d.Content)
                .NotEmpty().WithMessage("Content is required")
                .MaximumLength(5000).WithMessage("Content must not exceed 5000 characters");
        }
    }
}