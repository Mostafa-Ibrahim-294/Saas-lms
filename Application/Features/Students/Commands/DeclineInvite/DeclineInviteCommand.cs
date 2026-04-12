using Application.Features.TenantStudents.Dtos;

namespace Application.Features.Students.Commands.DeclineInvite
{
    public sealed record DeclineInviteCommand(string Token) : IRequest<OneOf<StudentResponse, Error>>;
}