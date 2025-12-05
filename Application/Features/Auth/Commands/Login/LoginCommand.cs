using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands.Login
{
    public sealed record LoginCommand(string Email, string Password)
        : IRequest<OneOf<LoginDto, Error>>;
}
