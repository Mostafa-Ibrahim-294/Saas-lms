using Application.Features.TenantStudents.Dtos;

namespace Application.Features.TenantStudents.Commands.AcceptInvite
{
    public sealed record AcceptInviteCommand(string Token) : IRequest<OneOf<StudentResponse, Error>>;
}
