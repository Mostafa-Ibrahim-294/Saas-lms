using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ModuleItems.Commands.UpdateQuiz
{
    public sealed class UpdateQuizCommandValidator : AbstractValidator<UpdateQuizCommand>
    {
        public UpdateQuizCommandValidator()
        {
            RuleForEach(x => x.Questions)
                .ChildRules(q => {
                q.RuleFor(x => x.Order)
               .GreaterThan(0);
             });
        }

    }
}
