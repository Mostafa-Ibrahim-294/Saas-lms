using Application.Features.StudentLessons.Dtos;

namespace Application.Features.StudentLessons.Commands.CreateStudentDiscussion
{
    public sealed record CreateStudentDiscussionCommand(int CourseId, int ItemId, string Content)
        : IRequest<OneOf<StudentLessonResponse, Error>>;
}