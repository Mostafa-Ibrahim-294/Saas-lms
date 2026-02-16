using Application.Features.Courses.Dtos;

namespace Application.Features.Courses.Queries.GetCourse
{
    public sealed record GetCourseQuery(int CourseId) : IRequest<OneOf<SingleCourseDto, Error>>;
}
