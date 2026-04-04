using Application.Features.TenantStudents.Dtos;

namespace Application.Features.TenantStudents.Commands.DeclineInvite
{
    public sealed record DeclineInviteCommand(string Token) : IRequest<OneOf<StudentResponse, Error>>;
}