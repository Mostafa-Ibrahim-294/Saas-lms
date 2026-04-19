using Application.Features.Discussions.Dtos;
using Application.Features.StudentLessons.Dtos;
using Domain.Enums;

namespace Application.Contracts.Repositories
{
    public interface IDiscussionRepository
    {
        Task<AllDiscussionsDto> GetAllDiscussionsAsync(string subDomain, string currentUser, string? Q, int? CourseId, ModuleItemType? Type, int? Cursor, int? Limit, CancellationToken cancellationToken);
        Task<List<DiscussionReplyDto>> GetDiscussionReplyAsync(int threadId, CancellationToken cancellationToken);
        Task<DicussionThread?> GetThreadTenantAsync(int threadId, string subDomain, CancellationToken cancellationToken);
        Task<DiscussionStatisticsDto> GetDiscussionStatisticsAsync(string subDomain, CancellationToken cancellationToken);
        Task CreateDiscussionThreadReadAsync(DicussionThreadRead dicussionThreadRead, CancellationToken cancellationToken);
        Task<int> SaveAsync(CancellationToken cancellationToken);
        Task<bool> DeleteDiscussionThreadAsync(int threadId, string subDomain, CancellationToken cancellationToken);
        Task<bool> DeleteDiscussionThreadReplyAsync(int threadId, int replyId, string subDomain, CancellationToken cancellationToken);
        Task<bool> UpdateDiscussionReplyAsync(int threadId, int replyId, string body, string subDomain, CancellationToken cancellationToken);
        Task<List<StudentDiscussionDto>> GetStudentDiscussionAsync(int itemId, int courseId, CancellationToken cancellationToken);
    }
}