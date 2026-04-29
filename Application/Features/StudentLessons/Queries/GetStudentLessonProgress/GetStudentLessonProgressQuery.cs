using Application.Features.StudentLessons.Dtos;

namespace Application.Features.StudentLessons.Queries.GetStudentLessonProgress
{
    public sealed record GetStudentLessonProgressQuery(int CourseId, int ItemId) : IRequest<OneOf<StudentLessonProgressDto, Error>>;
}