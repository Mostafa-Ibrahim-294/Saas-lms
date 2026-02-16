using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.Repositories
{
    public interface IModuleRepository
    {
        Task<int> GetMaxOrder(int courseId, CancellationToken cancellationToken);
        Task IncreaseOrder(int moduleId, int courseId, int minOrder, CancellationToken cancellationToken, int maxOrder = int.MaxValue);
        Task DecreaseOrder(int moduleId, int courseId, int minOrder, CancellationToken cancellationToken, int maxOrder = int.MaxValue);
        Task<int> CreateModule(Module module, CancellationToken cancellationToken);
        Task<Module?> GetModuleByIdAsync(int moduleId, CancellationToken cancellationToken);
        Task RemoveModule(Module module, CancellationToken cancellationToken);
    }
}
