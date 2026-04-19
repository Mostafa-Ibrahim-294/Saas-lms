using Application.Features.StudentLessons.Dtos;

namespace Application.Features.StudentLessons.Queries.GetStudentLessonItem
{
    public sealed record GetStudentLessonItemQuery(int CourseId, int ItemId) : IRequest<OneOf<StudentLessonItemDto, Error>>;
}