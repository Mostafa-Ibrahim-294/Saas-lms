using Application.Features.TenantStudents.Dtos;

namespace Application.Contracts.Repositories
{
    public interface ICourseInviteRepository
    {
        Task CreateCourseInviteAsync(CourseInvite courseInvite, CancellationToken cancellationToken);
        Task<int> SaveAsync(CancellationToken cancellationToken);
        Task<ValidateStudentInviteDto?> GetValidateStudentInviteAsync(string token, CancellationToken cancellationToken);
        Task<bool> IsValidTokenAsync(string token, CancellationToken cancellationToken);
        Task<string?> GetInvitedMemberEmailAsync(string token, CancellationToken cancellationToken);
        Task<CourseInvite?> GetPendingInviteAsync(string email, int courseId, string subDomain,CancellationToken cancellationToken);
        Task<int> GetCourseIdByTokenAsync(string token, CancellationToken cancellationToken);
        Task AcceptInviteAsync(string token, CancellationToken cancellationToken);
        Task DeclineInviteAsync(string token, CancellationToken cancellationToken);
        Task<int> GetTenantIdByInviteTokenAsync(string Token, CancellationToken cancellationToken);
    }
}