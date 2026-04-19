using Application.Features.StudentCourse.Dtos;

namespace Application.Features.StudentCourse.Queries.GetStudentCourseLiveSessions
{
    public sealed record GetStudentCourseLiveSessionsQuery(int CourseId) : IRequest<OneOf<List<StudentCourseLiveSessionDto>,Error>>;
}