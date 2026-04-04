using Application.Features.TenantStudents.Dtos;

namespace Application.Contracts.Repositories
{
    public interface IStudentRepository
    {
        Task<Student?> GetStudentAsync(int studentId, CancellationToken cancellationToken);
        Task<int> GetStudentIdAsync(string userId, CancellationToken cancellationToken);
        Task<List<StudentDto>> GetStudentsByCourseIdAsync(int courseId, CancellationToken cancellationToken);
        Task<StudentStatisticsDto> GetStudentStatisticsAsync(string subDomain, CancellationToken cancellationToken);
        Task<bool> DeleteStudentAsync(int studentId, int courseId, CancellationToken cancellationToken);
        Task<string> GetStudentUserIdAsync(int studentId, CancellationToken cancellationToken);
    }
}