using Application.Features.StudentUsers.Dtos;

namespace Application.Features.StudentUsers.Queries.GetStudentStreak
{
    public sealed record GetStudentStreakQuery : IRequest<OneOf<StudentStreakDto, Error>>;
}