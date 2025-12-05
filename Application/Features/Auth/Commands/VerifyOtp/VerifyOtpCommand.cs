using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Commands.VerifyOtp
{
    public sealed record VerifyOtpCommand(string Email, string OtpCode)
        : IRequest<OneOf<bool, Error>>;
}
