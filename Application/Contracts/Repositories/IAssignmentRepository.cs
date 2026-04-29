using Application.Features.Assignments.Dtos;

namespace Application.Contracts.Repositories
{
    public interface IAssignmentRepository
    {
        Task<List<StudentSubmissionDto>> GetSubmissionsAsync(int courseId, int itemId, CancellationToken cancellationToken);
        Task<OverviewDto?> GetOverviewAsync(int itemId, CancellationToken cancellationToken);
    }
}
