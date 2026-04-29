namespace Application.Contracts.Repositories
{
    public interface IStudentSubscriptionRepository
    {
        Task CreateSubscriptionAsync(StudentSubscription subscription, CancellationToken cancellationToken);
        Task<bool> StudentSubscriptionIsActiveAsync(int studentId, int courseId, CancellationToken cancellationToken);
    }
}