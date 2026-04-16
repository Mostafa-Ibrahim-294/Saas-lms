using Application.Features.ModuleItems.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enums;

namespace Infrastructure.Repositories
{
    internal sealed class ModuleItemRepository : IModuleItemRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public ModuleItemRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task AddTenantQuestionsAsync(int quizId, IEnumerable<QuizQuestion> questions, CancellationToken cancellationToken)
        {
            await _dbContext.QuizQuestions.Where(q => q.QuizId == quizId).ExecuteDeleteAsync(cancellationToken);
            await _dbContext.QuizQuestions.AddRangeAsync(questions, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task CreateAssignment(Assignment assignment, CancellationToken cancellationToken)
        {
            await _dbContext.Assignments.AddAsync(assignment);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task CreateLesson(Lesson lesson, CancellationToken cancellationToken)
        {
            await _dbContext.Lessons.AddAsync(lesson);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> CreateModuleItem(ModuleItem moduleItem, CancellationToken cancellationToken)
        {
            await _dbContext.ModuleItems.AddAsync(moduleItem);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return moduleItem.Id;
        }
        public async Task CreateQuiz(Quiz quiz, CancellationToken cancellationToken)
        {
            await _dbContext.Quizzes.AddAsync(quiz);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<List<AllItemsDto>> GetAllItemsAsync(int moduleId, ModuleItemType? type, CancellationToken cancellationToken)
        {
            var query = _dbContext.ModuleItems
                .AsNoTracking()
                .Where(mi => mi.ModuleId == moduleId);
            if (type.HasValue)
            {
                query = query.Where(mi => mi.Type == type.Value);
            }
            return await query
                .ProjectTo<AllItemsDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
        public async Task<AssignmentDto?> GetAssignmentAsync(int moduleItemId, CancellationToken cancellationToken)
        {
            return await _dbContext.Assignments
                .AsNoTracking()
                .Where(tm => tm.ModuleItemId == moduleItemId)
                .ProjectTo<AssignmentDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<Assignment?> GetAssignmentByModuleItemIdAsync(int moduleItemId, CancellationToken cancellationToken)
        {
            return await _dbContext.Assignments.
                 Include(x => x.ModuleItem).
                 FirstOrDefaultAsync(l => l.ModuleItemId == moduleItemId, cancellationToken);
        }
        public async Task<ModuleItem?> GetAsync(int moduleItemId, CancellationToken cancellationToken)
        {
            return await _dbContext.ModuleItems.AsNoTracking().FirstOrDefaultAsync(m => m.Id == moduleItemId, cancellationToken);

        }
        public async Task<ModuleItem?> GetItemConditions(int moduleItemId, CancellationToken cancellationToken)
        {
            return await _dbContext.ModuleItems
                .Include(mi => mi.Conditions)
                .FirstOrDefaultAsync(m => m.Id == moduleItemId, cancellationToken);
        }
        public async Task<Lesson?> GetLessonByModuleItemIdAsync(int moduleItemId, CancellationToken cancellationToken)
        {
            return await _dbContext.Lessons.
                 Include(x => x.ModuleItem).
                 FirstOrDefaultAsync(l => l.ModuleItemId == moduleItemId, cancellationToken);
        }
        public async Task<Quiz?> GetQuizAsync(int moduleItemId, CancellationToken cancellationToken)
        {
            return await _dbContext.Quizzes.
                 FirstOrDefaultAsync(l => l.ModuleItemId == moduleItemId, cancellationToken);

        }
        public async Task<QuizDto?> GetQuizWithQuestions(int moduleItemId, CancellationToken cancellationToken)
        {
            return await _dbContext.Quizzes
                .AsNoTracking()
                .Where(q => q.ModuleItemId == moduleItemId)
                .ProjectTo<QuizDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<SettingsDto?> GetSettingsAsync(int moduleItemId, CancellationToken cancellationToken)
        {
            return await _dbContext.ModuleItems
                .AsNoTracking()
                .Where(tm => tm.Id == moduleItemId)
                .ProjectTo<SettingsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public async Task RemoveAsync(ModuleItem moduleItem, CancellationToken cancellationToken)
        {
            _dbContext.ModuleItems.Remove(moduleItem);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<int?> GetFirstModuleItemAsync(int? moduleId, CancellationToken cancellationToken)
        {
            if (moduleId is null)
                return null;

            return await _dbContext.ModuleItems
                .Where(mt => mt.ModuleId == moduleId && mt.Order == 1)
                .Select(mt => (int?)mt.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}