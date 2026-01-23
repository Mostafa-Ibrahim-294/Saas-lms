using Infrastructure.Constants;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace Infrastructure.Services
{
    public sealed class EmailSender : IEmailSender
    {
        private readonly IOptions<MailOptions> _mailOptions;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IOptions<MailOptions> mailOptions, ILogger<EmailSender> logger)
        {
            _mailOptions = mailOptions;
            _logger = logger;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _logger.LogInformation(" Starting to send email via Brevo API to {Email}", email);

            var client = new RestClient(_mailOptions.Value.BaseUrl);
            var request = new RestRequest(_mailOptions.Value.Endpoint, Method.Post);

            request.AddHeader(BrevoHeaders.Accept, BrevoHeaders.AcceptValue);
            request.AddHeader(BrevoHeaders.ApiKey, _mailOptions.Value.BrevoApiKey);
            request.AddHeader(BrevoHeaders.ContentType, BrevoHeaders.ContentTypeValue);

            var body = new
            {
                sender = new
                {
                    name = _mailOptions.Value.FromName,
                    email = _mailOptions.Value.Email
                },
                to = new[]
                {
                        new { email = email }
                    },
                subject = subject,
                htmlContent = htmlMessage
            };

            request.AddJsonBody(JsonConvert.SerializeObject(body));
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
                _logger.LogInformation("Email sent successfully via Brevo API to {Email}", email);
            else
                _logger.LogError("Brevo API error: {StatusCode} - {Content}", response.StatusCode, response.Content);
        }
    }
}
