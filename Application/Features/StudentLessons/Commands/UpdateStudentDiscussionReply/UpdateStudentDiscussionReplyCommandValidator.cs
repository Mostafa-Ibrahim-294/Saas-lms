using FluentValidation;

namespace Application.Features.StudentLessons.Commands.UpdateStudentDiscussionReply
{
    public sealed class UpdateStudentDiscussionReplyCommandValidator : AbstractValidator<UpdateStudentDiscussionReplyCommand>
    {
        public UpdateStudentDiscussionReplyCommandValidator()
        {
            RuleFor(dr => dr.Content)
                .NotEmpty().WithMessage("Content is required")
                .MaximumLength(3000).WithMessage("Content must not exceed 3000 characters");
        }
    }
}
