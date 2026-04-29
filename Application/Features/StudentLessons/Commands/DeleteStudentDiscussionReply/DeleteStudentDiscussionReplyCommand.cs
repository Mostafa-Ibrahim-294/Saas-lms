using Application.Features.StudentLessons.Dtos;

namespace Application.Features.StudentLessons.Commands.DeleteStudentDiscussionReply
{
    public sealed record DeleteStudentDiscussionReplyCommand(int CourseId, int ItemId, int DiscussionId, int ReplyId)
        : IRequest<OneOf<StudentLessonResponse, Error>>;
}