using Application.Features.Students.Dtos;
using Application.Features.TenantStudents.Dtos;

namespace Application.Contracts.Repositories
{
    public interface IStudentRepository
    {
        Task CreateStudentAsync(Student student, CancellationToken cancellationToken);
        Task<Student?> GetStudentAsync(int studentId, CancellationToken cancellationToken);
        Task<int> GetStudentIdAsync(string userId, CancellationToken cancellationToken);
        Task<List<StudentsDto>> GetStudentsAsync(string subDomain, CancellationToken cancellationToken, int? courseId = null);
        Task<StudentStatisticsDto> GetStudentStatisticsAsync(string subDomain, CancellationToken cancellationToken);
        Task<bool> DeleteStudentAsync(int studentId, int courseId, CancellationToken cancellationToken);
        Task<StudentDto?> GetTenantStudentAsync(int studentId, string subDomain, CancellationToken cancellationToken);
        Task<List<AvailableSubjectDto>> GetAvailableSubjectsAsync(CancellationToken cancellationToken);
        Task UpdateHasOnboardedAsync(string userId, CancellationToken cancellationToken);
        Task<string> GetStudentEmailAsync(string userId, CancellationToken cancellationToken);
        Task<string> GetStuentNameByIdAsync(int studentId, CancellationToken cancellationToken);
        Task<Student?> GetStudentByInviteCodeAsync(string inviteCode, CancellationToken cancellationToken);
        Task<Student> GetStudentByIdAsync(int studentId, CancellationToken cancellationToken);
    }
}