using Application.Features.StudentLessons.Dtos;

namespace Application.Features.StudentLessons.Commands.UpdateStudentDiscussionReply
{
    public sealed record UpdateStudentDiscussionReplyCommand(int CourseId, int ItemId, int DiscussionId, int ReplyId, string Content)
        : IRequest<OneOf<StudentLessonResponse, Error>>;
}