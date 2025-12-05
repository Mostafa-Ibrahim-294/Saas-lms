using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.Errors
{
    public static class UserErrors
    {
        public static Error UserAlreadyExists =>
            new Error("User.AlreadyExists", "Email already exists", HttpStatusCode.Conflict);
        public static Error InvalidOtpCode =>
            new Error("User.InvalidOtpCode", "Invalid OTP code", HttpStatusCode.BadRequest);
    }
}
