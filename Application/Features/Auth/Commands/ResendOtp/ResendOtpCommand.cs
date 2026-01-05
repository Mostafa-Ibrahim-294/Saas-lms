using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands.ResendOtp
{
    public sealed record ResendOtpCommand : IRequest<OneOf<bool, Error>>;
}
