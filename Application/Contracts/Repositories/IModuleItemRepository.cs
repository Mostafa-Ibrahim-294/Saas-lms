using Application.Features.ModuleItems.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.Repositories
{
    public interface IModuleItemRepository
    {
        Task<int> CreateModuleItem(ModuleItem moduleItem, CancellationToken cancellationToken);
        Task CreateLesson(Lesson lesson, CancellationToken cancellationToken);
        Task CreateAssignment(Assignment assignment, CancellationToken cancellationToken);
         Task<Lesson?> GetLessonByModuleItemIdAsync(int moduleItemId, CancellationToken cancellationToken);
         Task<Assignment?> GetAssignmentByModuleItemIdAsync(int moduleItemId, CancellationToken cancellationToken);
         Task RemoveAsync(ModuleItem moduleItem, CancellationToken cancellationToken);
         Task<ModuleItem?> GetAsync(int moduleItemId, CancellationToken cancellationToken);
         Task<AssignmentDto?> GetAssignmentAsync(int moduleItemId, CancellationToken cancellationToken); 
         Task<SettingsDto?> GetSettingsAsync(int moduleItemId, CancellationToken cancellationToken);
        Task<List<AllItemsDto>> GetAllItemsAsync(int moduleId, ModuleItemType? type, CancellationToken cancellationToken);
        Task<ModuleItem?> GetItemConditions(int moduleItemId, CancellationToken cancellationToken);
    }
}
