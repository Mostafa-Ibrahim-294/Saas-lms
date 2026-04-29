<<<<<<< HEAD
﻿using Application.Features.Modules.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.Repositories
=======
﻿namespace Application.Contracts.Repositories
>>>>>>> 4c7a93aa4a11710a64ff2df81ec9e472ae2910a1
{
    public interface IModuleRepository
    {
        Task<int> GetMaxOrder(int courseId, CancellationToken cancellationToken);
        Task IncreaseOrder(int moduleId, int courseId, int minOrder, CancellationToken cancellationToken, int maxOrder = int.MaxValue);
        Task DecreaseOrder(int moduleId, int courseId, int minOrder, CancellationToken cancellationToken, int maxOrder = int.MaxValue);
        Task<int> CreateModule(Module module, CancellationToken cancellationToken);
        Task<Module?> GetModuleByIdAsync(int moduleId, int courseId, string subdomain, CancellationToken cancellationToken);
        Task<ModuleDto?> GetModuleWithItemsAsync(int moduleId, int courseId, string subdomain, CancellationToken cancellationToken);
        Task RemoveModule(Module module, CancellationToken cancellationToken);
<<<<<<< HEAD
        Task<List<AllModulesDto>> GetAllModulesAsync(int courseId, CancellationToken cancellationToken);
=======
        Task<int?> GetFirstModuleIdAsync(int courseId, CancellationToken cancellationToken);
>>>>>>> 4c7a93aa4a11710a64ff2df81ec9e472ae2910a1
    }
}