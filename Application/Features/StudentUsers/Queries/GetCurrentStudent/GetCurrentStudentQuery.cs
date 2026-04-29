using Application.Features.StudentUsers.Dtos;

namespace Application.Features.StudentUsers.Queries.GetCurrentStudent
{
    public sealed class GetCurrentStudentQuery : IRequest<OneOf<CurrentStudentDto, Error>>;
}