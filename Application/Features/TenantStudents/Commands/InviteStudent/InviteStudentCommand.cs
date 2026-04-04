using Application.Features.TenantStudents.Dtos;

namespace Application.Features.TenantStudents.Commands.InviteStudent
{
    public sealed record InviteStudentCommand(string StudentEmail, int CourseId) : IRequest<OneOf<StudentResponse, Error>>;
}