using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands.ForgetPassword
{
    public sealed record ForgetPasswordCommand(string Email)
        : IRequest<OneOf<bool, Error>>;
}
