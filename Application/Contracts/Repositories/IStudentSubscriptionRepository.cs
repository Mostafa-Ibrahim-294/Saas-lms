namespace Application.Contracts.Repositories
{
    public interface IStudentSubscriptionRepository
    {
        Task CreateSubscriptionAsync(StudentSubscription subscription, CancellationToken cancellationToken);
    }
}