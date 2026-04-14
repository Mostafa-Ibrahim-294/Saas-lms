using Application.Features.StudentCourse.Dtos;

namespace Application.Features.StudentCourse.Queries.GetStudentCourses
{
    public sealed record GetStudentCoursesQuery : IRequest<OneOf<List<StudentCourseDto>, Error>>;
}