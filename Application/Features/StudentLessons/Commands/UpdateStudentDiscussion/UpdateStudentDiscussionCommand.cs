using Application.Features.StudentLessons.Dtos;

namespace Application.Features.StudentLessons.Commands.UpdateStudentDiscussion
{
    public sealed record UpdateStudentDiscussionCommand(int CourseId, int ItemId, int DiscussionId, string Content)
        : IRequest<OneOf<StudentLessonResponse, Error>>;
}