using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    internal sealed class ModuleRepository : IModuleRepository
    {
        private readonly AppDbContext _context;
        public ModuleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> CreateModule(Module module, CancellationToken cancellationToken)
        {
            await _context.Modules.AddAsync(module, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return module.Id;
        }

        public async Task DecreaseOrder(int moduleId, int courseId, int minOrder, CancellationToken cancellationToken, int maxOrder = int.MinValue)
        {
            await _context.Modules.Where(m => m.CourseId == courseId && m.Order > minOrder && m.Order <= maxOrder && m.Id != moduleId)
           .ExecuteUpdateAsync(m => m.SetProperty(p => p.Order, p => p.Order - 1), cancellationToken);
        }

        public async Task<int> GetMaxOrder(int courseId, CancellationToken cancellationToken)
        {
            var maxOrder = await _context.Modules.Where(m => m.CourseId == courseId).MaxAsync(m => (int?)m.Order, cancellationToken);
            return maxOrder ?? 0;
        }

        public async Task<Module?> GetModuleByIdAsync(int moduleId, CancellationToken cancellationToken)
        {
            return await _context.Modules.FirstOrDefaultAsync(c => c.Id == moduleId, cancellationToken);
        }

        public async Task IncreaseOrder(int moduleId, int courseId, int minOrder, CancellationToken cancellationToken, int maxOrder = int.MaxValue)
        {
             await _context.Modules.Where(m => m.CourseId == courseId && m.Order >= minOrder && m.Order < maxOrder && m.Id != moduleId)
            .ExecuteUpdateAsync(m => m.SetProperty(p => p.Order, p => p.Order + 1), cancellationToken);
        }

        public async Task RemoveModule(Module module, CancellationToken cancellationToken)
        {
            _context.Modules.Remove(module);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
