using Application.Features.StudentLessons.Dtos;

namespace Application.Features.StudentLessons.Queries.GetStudentDiscussions
{
    public sealed record GetStudentDiscussionsQuery(int CourseId, int ItemId) : IRequest<OneOf<List<StudentDiscussionDto>, Error>>;
}