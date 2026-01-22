using Infrastructure.Constants;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RestSharp;

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
                if (string.IsNullOrWhiteSpace(_mailOptions.Value.BrevoApiKey))
                    return HealthCheckResult.Unhealthy("Mail server is unreachable.");

                var client = new RestClient(_mailOptions.Value.BaseUrl);
                var request = new RestRequest("account", Method.Get);
                request.AddHeader(BrevoHeaders.ApiKey, _mailOptions.Value.BrevoApiKey);

                var response = await client.ExecuteAsync(request, cancellationToken);

                if (response.IsSuccessful)
                    return HealthCheckResult.Healthy("Mail server is reachable.");
                else
                    return HealthCheckResult.Unhealthy($"Brevo API returned error: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"Mail server is unreachable: {ex.Message}");
            }
        }
    }
}
