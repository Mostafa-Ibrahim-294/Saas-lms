using Application.Features.Questions.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    internal sealed class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public QuestionRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> CreateQuestion(Question question, CancellationToken cancellationToken)
        {
            await _dbContext.Questions.AddAsync(question, cancellationToken);
            await SaveAsync(cancellationToken);
            return question.Id;
        }

        public async Task CreateQuestionCategory(QuestionCategory category, CancellationToken cancellationToken)
        {
            await _dbContext.Categories.AddAsync(category, cancellationToken);
            await SaveAsync(cancellationToken);
        }

        public async Task<IEnumerable<AllQuestionsDto>> GetAllQuestions(string subdomain, CancellationToken cancellationToken)
        {
            return await _dbContext.Questions.AsNoTracking()
                .Where(q => q.Tenant.SubDomain == subdomain)
                .ProjectTo<AllQuestionsDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<Question?> GetQuestion(int id, string subdomain, CancellationToken cancellationToken)
        {
            return await _dbContext.Questions
                .Include(d => d.QuestionCategory)
                .FirstOrDefaultAsync(q => q.Id == id && q.Tenant.SubDomain == subdomain, cancellationToken);
        }

        public async Task<IEnumerable<CategoryStatisticsDto>> GetQuestionCategories(string subdomain, CancellationToken cancellationToken)
        {
            return await _dbContext.Categories.AsNoTracking()
                .Where(c => c.Tenant.SubDomain == subdomain)
                .Select(c => new CategoryStatisticsDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    QuestionCount = c.Questions.Count
                }).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<QuestionTypeDto>> GetQuestionsByType(string subdomain, CancellationToken cancellationToken)
        {
            return await _dbContext.Questions.AsNoTracking()
                .Where(q => q.Tenant.SubDomain == subdomain)
                .GroupBy(q => q.Type)
                .Select(g => new QuestionTypeDto
                {
                    Type = g.Key,
                    Count = g.Count()
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<List<QuestionCategoryDto>> GetQuestionWithCategory(string subdomain, CancellationToken cancellationToken)
        {
            return await _dbContext.Categories.AsNoTracking()
                .Where(c => c.Tenant.SubDomain == subdomain)
                .ProjectTo<QuestionCategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> GetTotalQuestions(string subdomain, CancellationToken cancellationToken)
        {
            return await _dbContext.Questions.CountAsync(q => q.Tenant.SubDomain == subdomain, cancellationToken);
        }

        public async Task<int> GetUsedQuestions(string subdomain, CancellationToken cancellationToken)
        {
            return await _dbContext.Questions.CountAsync(q => q.Tenant.SubDomain == subdomain && q.Reuse > 0, cancellationToken);
        }

        public async Task<int> GetWeekQuestions(string subdomain, CancellationToken cancellationToken)
        {
            return await _dbContext.Questions.CountAsync(q => q.Tenant.SubDomain == subdomain && q.CreatedAt >= DateTime.UtcNow.AddDays(-7), cancellationToken);
        }

        public async Task<bool> IsFoundCategory(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Categories.AnyAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<bool> IsUniqueCategory(string title, string subdomain, CancellationToken cancellationToken)
        {
            return !await _dbContext.Categories.AnyAsync(c => c.Title == title && c.Tenant.SubDomain == subdomain, cancellationToken);
        }
        public async Task RemoveAsync(Question question, CancellationToken cancellationToken)
        {
            _dbContext.Questions.Remove(question);
            await SaveAsync(cancellationToken);
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
