using Application.Features.TenantStudents.Dtos;

namespace Application.Features.Students.Commands.AcceptInvite
{
    public sealed record AcceptInviteCommand(string Token) : IRequest<OneOf<StudentResponse, Error>>;
}
