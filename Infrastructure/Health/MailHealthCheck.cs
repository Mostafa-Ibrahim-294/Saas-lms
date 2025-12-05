using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Health
{
    public sealed class MailHealthCheck : IHealthCheck
    {
        private readonly IOptions<MailOptions> _mailOptions;
        public MailHealthCheck(IOptions<MailOptions> mailOptions)
        {
            _mailOptions = mailOptions;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_mailOptions.Value.SmtpServer, _mailOptions.Value.Port, SecureSocketOptions.StartTls, cancellationToken);
                await smtp.AuthenticateAsync(_mailOptions.Value.Email, _mailOptions.Value.Password, cancellationToken);
                await smtp.DisconnectAsync(true, cancellationToken);
            }
            catch
            {
                return HealthCheckResult.Unhealthy("Mail server is unreachable.");
            }
            return HealthCheckResult.Healthy("Mail server is reachable.");
        }
    }
}
