using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Common.Options
{
    public sealed class MailOptions
    {
        public string SmtpServer { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
