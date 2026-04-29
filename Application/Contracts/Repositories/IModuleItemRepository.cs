<<<<<<< HEAD
﻿using Application.Features.ModuleItems.Commands.ReorderModuleItem;
using Application.Features.ModuleItems.Dtos;
=======
﻿using Application.Features.ModuleItems.Dtos;
using Application.Features.StudentLessons.Dtos;
>>>>>>> 4c7a93aa4a11710a64ff2df81ec9e472ae2910a1
using Domain.Enums;

namespace Application.Contracts.Repositories
{
    public interface IModuleItemRepository
    {
        Task<int> CreateModuleItem(ModuleItem moduleItem, CancellationToken cancellationToken);
        Task CreateLesson(Lesson lesson, CancellationToken cancellationToken);
        Task CreateAssignment(Assignment assignment, CancellationToken cancellationToken);
        Task CreateQuiz(Quiz quiz, CancellationToken cancellationToken);
<<<<<<< HEAD
        Task<Lesson?> GetLessonByModuleItemIdAsync(int moduleItemId, int moduleId, int courseId, string subdomain, CancellationToken cancellationToken);
         Task<Assignment?> GetAssignmentByModuleItemIdAsync(int moduleItemId, int moduleId, int courseId, string subdomain, CancellationToken cancellationToken);
         Task RemoveAsync(ModuleItem moduleItem, CancellationToken cancellationToken);
         Task<ModuleItem?> GetAsync(int moduleItemId, int moduleId, int courseId, string subdomain, CancellationToken cancellationToken);
         Task<AssignmentDto?> GetAssignmentAsync(int moduleItemId, int moduleId, int courseId, string subdomain, CancellationToken cancellationToken); 
         Task<SettingsDto?> GetSettingsAsync(int moduleItemId, int moduleId, int courseId, string subdomain, CancellationToken cancellationToken);
        Task<List<AllItemsDto>> GetAllItemsAsync(int moduleId, ModuleItemType? type,CancellationToken cancellationToken);
        Task<ModuleItem?> GetItemConditions(int moduleItemId, int moduleId, int courseId, string subdomain, CancellationToken cancellationToken);
        Task<Quiz?> GetQuizAsync(int moduleItemId, int moduleId, int courseId, string subdomain, CancellationToken cancellationToken);
        Task<QuizDto?> GetQuizWithQuestions(int moduleItemId, int moduleId, int courseId, string subdomain, CancellationToken cancellationToken);
        Task AddTenantQuestionsAsync(int quizId, IEnumerable<QuizQuestion> questions, CancellationToken cancellationToken);
        Task ReorderItems(IEnumerable<OrderDto> orders, CancellationToken cancellationToken);
=======
        Task<Lesson?> GetLessonByModuleItemIdAsync(int moduleItemId, CancellationToken cancellationToken);
        Task<Assignment?> GetAssignmentByModuleItemIdAsync(int moduleItemId, CancellationToken cancellationToken);
        Task RemoveAsync(ModuleItem moduleItem, CancellationToken cancellationToken);
        Task<ModuleItem?> GetAsync(int moduleItemId, CancellationToken cancellationToken);
        Task<AssignmentDto?> GetAssignmentAsync(int moduleItemId, CancellationToken cancellationToken);
        Task<SettingsDto?> GetSettingsAsync(int moduleItemId, CancellationToken cancellationToken);
        Task<List<AllItemsDto>> GetAllItemsAsync(int moduleId, ModuleItemType? type, CancellationToken cancellationToken);
        Task<ModuleItem?> GetItemConditions(int moduleItemId, CancellationToken cancellationToken);
        Task<Quiz?> GetQuizAsync(int moduleItemId, CancellationToken cancellationToken);
        Task<QuizDto?> GetQuizWithQuestions(int moduleItemId, CancellationToken cancellationToken);
        Task AddTenantQuestionsAsync(int quizId, IEnumerable<QuizQuestion> questions, CancellationToken cancellationToken);
        Task<int?> GetFirstModuleItemAsync(int? moduleId, CancellationToken cancellationToken);
        Task<bool> ModuleItemIsExistAsync(int moduleItemId, int courseId, CancellationToken cancellationToken);
        Task<StudentLessonItemDto> GetStudentLessonItemAsync(int moduleItemId, int courseId, CancellationToken cancellationToken);
        Task<int> GetModuleIdAsync(int itemId, int courseId, CancellationToken cancellationToken);
>>>>>>> 4c7a93aa4a11710a64ff2df81ec9e472ae2910a1
    }
}