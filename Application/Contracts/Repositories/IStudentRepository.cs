using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<string>> GetStudentsEmails(IEnumerable<int> studentIds, string subdomain, CancellationToken cancellationToken);
    }
}
