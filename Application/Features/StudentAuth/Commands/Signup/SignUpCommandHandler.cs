using Application.Contracts.Repositories;
using Application.Helpers;
using Hangfire;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Application.Features.StudentAuth.Commands.Signup
{
    internal sealed class SignUpCommandHandler : IRequestHandler<SignUpCommand, OneOf<bool, Error>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly HybridCache _hybridCache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStudentRepository _studentRepository;

        public SignUpCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper, IEmailSender emailSender,
            HybridCache hybridCache, IHttpContextAccessor httpContextAccessor, IStudentRepository studentRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailSender = emailSender;
            _hybridCache = hybridCache;
            _httpContextAccessor = httpContextAccessor;
            _studentRepository = studentRepository;
        }
        public async Task<OneOf<bool, Error>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser is not null)
                return UserErrors.UserAlreadyExists;

            var newUser = _mapper.Map<ApplicationUser>(request);
            newUser.EmailConfirmed = true;

            await _studentRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var createdResult = await _userManager.CreateAsync(newUser);
                if (!createdResult.Succeeded)
                {
                    await _studentRepository.RollbackTransactionAsync(cancellationToken);
                    var error = string.Join(", ", createdResult.Errors.Select(e => e.Description).First());
                    return new Error("UserCreationFailed", error, HttpStatusCode.BadRequest);
                }
                await _userManager.AddToRoleAsync(newUser, RoleConstants.Student);

                var newStudent = _mapper.Map<Student>(request);
                newStudent.UserId = newUser.Id;

                await _studentRepository.CreateStudentAsync(newStudent, cancellationToken);
                await _studentRepository.CommitTransactionAsync(cancellationToken);

                var otpCode = await GenerateOtpHelper.GenerateOtp(request.Email, _hybridCache, _httpContextAccessor, cancellationToken);
                var emailBody = EmailConfirmationHelper.GenerateEmailBodyHelper(EmailConstants.OtpTemplate, new Dictionary<string, string>
                {
                    { "{{OTP_CODE}}", otpCode },
                    { "{{UserName}}", request.FirstName }
                });
                BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(request.Email, EmailConstants.EmailConfirmationSubject, emailBody));
                return true;
            }
            catch
            {
                await _studentRepository.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}