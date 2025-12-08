using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.Repositories.PlanRepository
{
    public interface IPlanRepository
    {
        Task<IEnumerable<Plan>> GetAllPlansWithDetailsAsync();

    }
}
