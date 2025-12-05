using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    internal sealed class RefreshRepository : IRefreshRepository
    {
        private readonly AppDbContext _context;
        public RefreshRepository(AppDbContext context)
        {
            _context = context;
        }   
        public async Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken cancellationToken)
        {
            return await _context.RefreshTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
