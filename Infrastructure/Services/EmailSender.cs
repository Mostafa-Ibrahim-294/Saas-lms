using Infrastructure.Common.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public sealed class EmailSender : IEmailSender
    {
        private readonly IOptions<MailOptions> _mailOptions;
        public EmailSender(IOptions<MailOptions> mailOptions)
        {
            _mailOptions = mailOptions;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage()
            {
                Sender = MailboxAddress.Parse(_mailOptions.Value.Email),
                Subject = subject
            };
            message.To.Add(MailboxAddress.Parse(email));
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = htmlMessage
            };
            message.Body = bodyBuilder.ToMessageBody();
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_mailOptions.Value.SmtpServer, _mailOptions.Value.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailOptions.Value.Email, _mailOptions.Value.Password);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
    }
}
