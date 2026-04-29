using FluentValidation;

namespace Application.Features.StudentLessons.Commands.CreateStudentDiscussionReply
{
    public sealed class CreateStudentDiscussionReplyCommandValidator : AbstractValidator<CreateStudentDiscussionReplyCommand>
    {
        public CreateStudentDiscussionReplyCommandValidator()
        {
            RuleFor(dr => dr.Content)
                .NotEmpty().WithMessage("Content is required")
                .MaximumLength(3000).WithMessage("Content must not exceed 3000 characters");
        }
    }
}
