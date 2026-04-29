using FluentValidation;

namespace Application.Features.Discussions.Commands.UpdateDiscussionReply
{
    public sealed class UpdateDiscussionReplyCommandValidator : AbstractValidator<UpdateDiscussionReplyCommand>
    {
        public UpdateDiscussionReplyCommandValidator()
        {
            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("Reply content is required.")
                .MinimumLength(3).WithMessage("Reply content is too short.")
                .MaximumLength(3000).WithMessage("Reply content must not exceed 3000 characters.")
                .Must(body => !string.IsNullOrWhiteSpace(body))
                .WithMessage("Reply content is invalid.");
        }
    }
}