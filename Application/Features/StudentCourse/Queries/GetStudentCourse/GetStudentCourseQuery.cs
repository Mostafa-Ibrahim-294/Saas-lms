using Application.Features.StudentCourse.Dtos;

namespace Application.Features.StudentCourse.Queries.GetStudentCourse
{
    public sealed record GetStudentCourseQuery(int CourseId) : IRequest<OneOf<StudentCourseDto, Error>>;
}