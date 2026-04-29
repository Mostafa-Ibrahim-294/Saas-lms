using Application.Features.StudentLessons.Dtos;

namespace Application.Features.StudentLessons.Commands.CreateStudentDiscussionReply
{
    public sealed record CreateStudentDiscussionReplyCommand(int CourseId, int ItemId, int DiscussionId, string Content)
        : IRequest<OneOf<StudentLessonResponse, Error>>;
}