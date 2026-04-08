using Application.Constants;
using Application.Features.Discussions.Dtos;
using Domain.Enums;

namespace Infrastructure.Repositories
{
    internal sealed class DiscussionRepository : IDiscussionRepository
    {
        private readonly AppDbContext _context;

        public DiscussionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AllDiscussionsDto> GetAllDiscussionsAsync(string subDomain, string currentUser, string? Q, int? CourseId, ModuleItemType? Type, int? Cursor, int? Limit, CancellationToken cancellationToken)
        {
            var pageSize = Limit ?? 8;

            var query = _context.DicussionThreads
                .AsNoTracking()
                .Where(dt => dt.Tenant.SubDomain == subDomain);

            if (!string.IsNullOrEmpty(Q))
                query = query.Where(dt => dt.Content.Contains(Q));

            if (CourseId.HasValue)
                query = query.Where(dt => dt.CourseId == CourseId.Value);

            if (Type.HasValue)
                query = query.Where(dt => dt.ItemType == Type.Value);

            var threads = await query
                .Where(dt => dt.Id < (Cursor ?? int.MaxValue))
                .OrderByDescending(dt => dt.Id)
                .Take(pageSize + 1)
                .Select(dt => new DiscussionDto
                {
                    Id = dt.Id,
                    CourseId = dt.CourseId,
                    CourseName = dt.Course.Title,
                    ItemId = dt.ItemId,
                    ModuleId = dt.ModuleId,
                    ModuleName = dt.ModuleItem.Module.Title,
                    AuthorName = dt.User.FirstName + " " + dt.User.LastName,
                    ItemTitle = dt.ModuleItem.Title,
                    ItemType = dt.ItemType,
                    CreatedBy = dt.CreatedBy,
                    CreatedAt = dt.CreatedAt,
                    Unread = !dt.DicussionReads.Any(dr => dr.UserId == currentUser),
                    RepliesCount = dt.RepliesCount,
                    LastActivityAt = dt.UpdatedAt
                })
                .ToListAsync(cancellationToken);

            var hasMore = threads.Count > pageSize;
            if (hasMore)
                threads.RemoveAt(threads.Count - 1);

            return new AllDiscussionsDto
            {
                Data = threads,
                HasMore = hasMore,
                NextCursor = threads.LastOrDefault()?.Id ?? 0
            };
        }
        public async Task<List<DiscussionReplyDto>> GetDiscussionReplyAsync(int threadId, CancellationToken cancellationToken)
        {
            var result = await (from reply in _context.DicussionThreadReplies
                                where reply.DicussionId == threadId
                                join ur in _context.UserRoles
                                on reply.AuthorId equals ur.UserId into userRoles
                                from ur in userRoles.DefaultIfEmpty()
                                join r in _context.Roles
                                on ur.RoleId equals r.Id into roles
                                from r in roles.DefaultIfEmpty()
                                select new DiscussionReplyDto
                                {
                                    Id = reply.Id,
                                    ThreadId = reply.DicussionId,
                                    Body = reply.Body,
                                    CreatedAt = reply.CreatedAt,
                                    Author = new DiscussionAuthorDto
                                    {
                                        Id = reply.User.Id,
                                        Name = reply.User.FirstName + " " + reply.User.LastName,
                                        Email = reply.User.Email!,
                                        Role = r.Name!
                                    }
                                }
                               ).ToListAsync(cancellationToken);
            return result;
        }
        public async Task<DicussionThread?> GetThreadTenantAsync(int threadId, string subDomain, CancellationToken cancellationToken)
        {
            return await _context.DicussionThreads
                .AsNoTracking()
                .Where(dt => dt.Id == threadId && dt.Tenant.SubDomain == subDomain)
                .Select(dt => new DicussionThread { Id = dt.Id, TenantId = dt.TenantId })
                .FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<DiscussionStatisticsDto> GetDiscussionStatisticsAsync(string subDomain, CancellationToken cancellationToken)
        {
            var totalDiscussionThreads = _context.DicussionThreads.AsNoTracking().Where(dt => dt.Tenant.SubDomain == subDomain);
            var totalDiscussionReplies = _context.DicussionThreadReplies.AsNoTracking().Where(dtr => dtr.Tenant.SubDomain == subDomain);

            var totalUnreadDiscussionThreads = totalDiscussionThreads.Where(dt => !dt.DicussionReads.Any(dr => dr.Tenant.SubDomain == subDomain));
            var unAnsweredDiscussionReplies = totalDiscussionThreads.Where(dt => !totalDiscussionReplies.Any(dtr => dtr.DicussionId == dt.Id));

            var tenantRoleId = await _context.Roles
                .Where(r => r.Name == RoleConstants.Tenant)
                .Select(r => r.Id)
                .FirstAsync(cancellationToken);

            var threads = await (from dt in totalDiscussionThreads
                                 let firstTenantReplyAt = (
                                     from r in _context.DicussionThreadReplies
                                     join ur in _context.UserRoles on r.AuthorId equals ur.UserId
                                     where r.DicussionId == dt.Id && ur.RoleId == tenantRoleId
                                     orderby r.CreatedAt
                                     select (DateTime?)r.CreatedAt
                                 ).FirstOrDefault()
                                 where firstTenantReplyAt != null
                                 select new { dt.CreatedAt, FirstReplyAt = (DateTime?)firstTenantReplyAt }
               ).ToListAsync(cancellationToken);

            var threadsLast24h = await totalDiscussionThreads
                .Where(dt => dt.CreatedAt >= DateTime.UtcNow.AddHours(-24))
                .CountAsync(cancellationToken);

            return new DiscussionStatisticsDto
            {
                TotalUnreadThreads = await totalUnreadDiscussionThreads.CountAsync(cancellationToken),
                UnansweredThreads = await unAnsweredDiscussionReplies.CountAsync(cancellationToken),
                AvgResponseTime = threads.Any() ? (int)threads
                        .Where(x => x.FirstReplyAt.HasValue)
                        .Average(x => (x.FirstReplyAt!.Value - x.CreatedAt).TotalSeconds)
                        : 0,
                ThreadsLast24h = threadsLast24h
            };
        }
        public async Task CreateDiscussionThreadReadAsync(DicussionThreadRead dicussionThreadRead, CancellationToken cancellationToken)
        {
            await _context.DicussionThreadReads.AddAsync(dicussionThreadRead, cancellationToken);
        }
        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> DeleteDiscussionThreadAsync(int threadId, string subDomain, CancellationToken cancellationToken)
        {
            var thread = await _context.DicussionThreads.FirstOrDefaultAsync(dt => dt.Id == threadId && dt.Tenant.SubDomain == subDomain);
            if (thread is null)
                return false;

            _context.DicussionThreads.Remove(thread);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<bool> DeleteDiscussionThreadReplyAsync(int threadId, int replyId, string subDomain, CancellationToken cancellationToken)
        {
            var reply = await _context.DicussionThreadReplies
                .FirstOrDefaultAsync(dtr => dtr.Id == replyId && dtr.DicussionId == threadId && dtr.Tenant.SubDomain == subDomain, cancellationToken);
            if(reply is null)
                return false;

            _context.DicussionThreadReplies.Remove(reply);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<bool> UpdateDiscussionReplyAsync(int threadId, int replyId, string body, string subDomain, CancellationToken cancellationToken)
        {
            var result = await _context.DicussionThreadReplies
            .Where(dtr => dtr.Id == replyId && dtr.DicussionId == threadId && dtr.Tenant.SubDomain == subDomain)
            .ExecuteUpdateAsync(setters => setters.SetProperty(r => r.Body, body), cancellationToken);

            if (result == 0)
             return false;

            return true;
        }
    }
}