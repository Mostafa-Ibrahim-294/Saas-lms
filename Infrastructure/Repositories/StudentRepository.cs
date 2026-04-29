using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public sealed class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _dbContext;
        public StudentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<string>> GetStudentsEmails(IEnumerable<int> studentIds, string subdomain, CancellationToken cancellationToken)
        {
            return await _dbContext.Students
                .Where(s => studentIds.Contains(s.Id) && s.TeachingLevel.Tenant.SubDomain == subdomain)
                .Select(s => s.User.Email!)
                .ToListAsync(cancellationToken);
        }
    }
}
