using Application.Features.TenantStudents.Dtos;

namespace Application.Features.TenantStudents.Commands.ValidateStudentInvite
{
    public sealed record ValidateStudentInviteCommand(string Token) : IRequest<OneOf<ValidateStudentInviteDto, Error>>;
}