using Application.Contracts.Repositories;
using Application.Features.TenantStudents.Dtos;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Features.TenantStudents.Commands.AcceptInvite
{
    internal sealed class AcceptInviteCommandHandler : IRequestHandler<AcceptInviteCommand, OneOf<StudentResponse, Error>>
    {
        private readonly ICourseInviteRepository _courseInviteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public AcceptInviteCommandHandler(ICourseInviteRepository courseInviteRepository,IHttpContextAccessor httpContextAccessor,
            IEnrollmentRepository enrollmentRepository)
        {
            _courseInviteRepository = courseInviteRepository;
            _httpContextAccessor = httpContextAccessor;
            _enrollmentRepository = enrollmentRepository;
        }
        public async Task<OneOf<StudentResponse, Error>> Handle(AcceptInviteCommand request, CancellationToken cancellationToken)
        {
            var isValidToken = await _courseInviteRepository.IsValidTokenAsync(request.Token, cancellationToken);
            if (!isValidToken)
                return CourseInviteErrors.InviteExpired;

            var studentEmail = _httpContextAccessor.HttpContext?.Session.GetString(AuthConstants.StudentEmail);
            if (studentEmail is null)
                return CourseInviteErrors.InviteError;

            var invitedEmail = await _courseInviteRepository.GetInvitedMemberEmailAsync(request.Token, cancellationToken);
            if (invitedEmail is null)
                return CourseInviteErrors.InviteError;

            if (!string.Equals(studentEmail, invitedEmail))
                return TenantInviteErrors.InviteError;

            var courseId = await _courseInviteRepository.GetCourseIdByTokenAsync(request.Token, cancellationToken);
            var studentId = _httpContextAccessor.HttpContext?.Session.GetInt32(AuthConstants.StudentId);
            var tenantId = await _courseInviteRepository.GetTenantIdAsync(request.Token, cancellationToken);

            var newEnrollment = new Enrollment
            {
                CourseId = courseId,
                StudentId = studentId!.Value,
                EnrollmentType = EnrollmentType.Invited,
                TenantId = tenantId
            };
            await _enrollmentRepository.CreateEnrollmentAsync(newEnrollment, cancellationToken);
            await _enrollmentRepository.SaveAsync(cancellationToken);
            await _courseInviteRepository.AcceptInviteAsync(request.Token, cancellationToken);
            return new StudentResponse { Message = CourseInviteConstants.AcceptInvite };
        }
    }
}