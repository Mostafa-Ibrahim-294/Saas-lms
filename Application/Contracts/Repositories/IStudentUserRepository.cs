using Application.Features.StudentUsers.Dtos;

namespace Application.Contracts.Repositories
{
    public interface IStudentUserRepository
    {
        Task<StudentUserProfileDto> GetUserProfileAsync(string userId, string role, CancellationToken cancellationToken);
    }
}