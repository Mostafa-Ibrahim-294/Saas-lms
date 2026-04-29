using Application.Features.StudentCourse.Dtos;

namespace Application.Features.StudentCourse.Queries.GetStudentCourseLiveSession
{
    public sealed record GetStudentCourseLiveSessionQuery(int CourseId, int SessionId)
        : IRequest<OneOf<StudentCourseLiveSessionDto, Error>>;
}