using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.Repositories
{
    public interface ISubscriptionRepository
    {
        Task CreateFreeSubcscription(int TenantId, Guid PlanPricingId, CancellationToken cancellationToken);
    }
}
