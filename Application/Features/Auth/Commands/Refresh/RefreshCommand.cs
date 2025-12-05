using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands.Refresh
{
    public sealed record RefreshCommand(string RefreshToken)
        : IRequest<OneOf<bool, Error>>;
}
