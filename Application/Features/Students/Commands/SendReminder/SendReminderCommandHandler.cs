using Hangfire;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Students.Commands.SendReminder
{
    internal sealed class SendReminderCommandHandler : IRequestHandler<SendReminderCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _contextAccessor;
        public SendReminderCommandHandler(IStudentRepository studentRepository, IEmailSender emailSender, IHttpContextAccessor contextAccessor)
        {
            _studentRepository = studentRepository;
            _emailSender = emailSender;
            _contextAccessor = contextAccessor;
        }
        public async Task Handle(SendReminderCommand request, CancellationToken cancellationToken)
        {
            var subdomain = _contextAccessor?.HttpContext?.Request.Cookies[AuthConstants.SubDomain];
            var studentsEmails = await _studentRepository.GetStudentsEmails(request.StudentIds, subdomain!, cancellationToken);
            foreach (var email in studentsEmails)
            {
                var currentEmail = email;
                BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(currentEmail, EmailConstants.StudentReminderSubject, request.Message));
            }
        }
    }
}
