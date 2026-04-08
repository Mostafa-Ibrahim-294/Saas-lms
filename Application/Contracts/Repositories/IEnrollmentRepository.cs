namespace Application.Contracts.Repositories
{
    public interface IEnrollmentRepository
    {
        Task CreateEnrollmentAsync(Enrollment enrollment, CancellationToken cancellationToken);
        Task<int> SaveAsync(CancellationToken cancellationToken);
        Task<bool> StudentIsAlreadyEnrolledAsync(int studentId, int courseId, CancellationToken cancellationToken);
        Task<List<string>> GetEmailsByCourseIdsAsync(int[] courseIds, CancellationToken cancellationToken);
        Task<List<string>> GetAllStudentEmailsAsync(int tenantId, CancellationToken cancellationToken);
    }
}