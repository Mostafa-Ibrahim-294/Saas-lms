using Application.Features.Students.Dtos;

namespace Application.Features.Students.Commands.ValidateStudentInvite
{
    public sealed record ValidateStudentInviteCommand(string Token) : IRequest<OneOf<ValidateStudentInviteDto, Error>>;
}