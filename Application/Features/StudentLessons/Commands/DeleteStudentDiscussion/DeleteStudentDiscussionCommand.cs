using Application.Features.StudentLessons.Dtos;

namespace Application.Features.StudentLessons.Commands.DeleteStudentDiscussion
{
    public sealed record DeleteStudentDiscussionCommand(int CourseId, int ItemId, int DiscussionId)
        : IRequest<OneOf<StudentLessonResponse, Error>>;
}