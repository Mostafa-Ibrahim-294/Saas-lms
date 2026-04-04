namespace Application.Contracts.Repositories
{
    public interface IEnrollmentRepository
    {
        Task CreateEnrollmentAsync(Enrollment enrollment, CancellationToken cancellationToken);
        Task<int> SaveAsync(CancellationToken cancellationToken);
        Task<bool> StudentIsAlreadyEnrolledAsync(int studentId, int courseId, CancellationToken cancellationToken);
    }
}