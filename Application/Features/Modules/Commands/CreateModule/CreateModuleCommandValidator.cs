using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Modules.Commands.CreateModule
{
    public sealed class CreateModuleCommandValidator : AbstractValidator<CreateModuleCommand>
    {
        public CreateModuleCommandValidator() 
        { 
            RuleFor(x => x.Order)
                .GreaterThan(0)
                .WithMessage("Order must be greater than 0");
        }
    }
}
