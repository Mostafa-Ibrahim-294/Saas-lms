using Application.Features.Students.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enums;

namespace Infrastructure.Repositories
{
    internal sealed class CourseInviteRepository : ICourseInviteRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CourseInviteRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task CreateCourseInviteAsync(CourseInvite courseInvite, CancellationToken cancellationToken)
        {
            await _context.CourseInvites.AddAsync(courseInvite, cancellationToken);
        }
        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<ValidateStudentInviteDto?> GetValidateStudentInviteAsync(string token, CancellationToken cancellationToken)
        {
            var result = await _context.CourseInvites
                .AsNoTracking()
                .Where(ti => ti.Token == token)
                .ProjectTo<ValidateStudentInviteDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
            return result;
        }
        public Task<bool> IsValidTokenAsync(string token, CancellationToken cancellationToken)
        {
            return _context.CourseInvites
                .AsNoTracking()
                .AnyAsync(ci => ci.Token == token &&
                    ci.ExpiresAt > DateTime.UtcNow &&
                    ci.AcceptedAt == null &&
                    ci.Status == TenantInviteStatus.Pending, cancellationToken);
        }
        public async Task<string?> GetInvitedMemberEmailAsync(string token, CancellationToken cancellationToken)
        {
            return await _context.CourseInvites
                .AsNoTracking()
                .Where(ti => ti.Token == token)
                .Select(ti => ti.Email)
                .FirstOrDefaultAsync(cancellationToken)!;
        }
        public async Task<CourseInvite?> GetPendingInviteAsync(string email, int courseId, string subDomain, CancellationToken cancellationToken)
        {
            return await _context.CourseInvites
                .AsNoTracking()
                .FirstOrDefaultAsync(ci => ci.Email == email &&
                    ci.CourseId == courseId && ci.Status == TenantInviteStatus.Pending &&
                    ci.Tenant.SubDomain == subDomain, cancellationToken);
        }
        public async Task<int> GetCourseIdByTokenAsync(string token, CancellationToken cancellationToken)
        {
            return await _context.CourseInvites
                .AsNoTracking()
                .Where(ci => ci.Token == token)
                .Select(ci => ci.CourseId)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public async Task AcceptInviteAsync(string token, CancellationToken cancellationToken)
        {
            await _context.CourseInvites
                .Where(ci => ci.Token == token && ci.Status == TenantInviteStatus.Pending &&
                    ci.ExpiresAt > DateTime.UtcNow && ci.AcceptedAt == null)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(ci => ci.AcceptedAt, DateTime.UtcNow)
                    .SetProperty(ci => ci.Status, TenantInviteStatus.Accepted),
                    cancellationToken);
        }
        public async Task DeclineInviteAsync(string token, CancellationToken cancellationToken)
        {
            await _context.CourseInvites
                .Where(ci => ci.Token == token && ci.Status == TenantInviteStatus.Pending 
                    && ci.ExpiresAt > DateTime.UtcNow && ci.AcceptedAt == null)
                .ExecuteUpdateAsync(setters => setters.SetProperty(ci => ci.Status, TenantInviteStatus.Declined), cancellationToken);
        }
        public async Task<int> GetTenantIdByInviteTokenAsync(string Token, CancellationToken cancellationToken)
        {
            return await _context.CourseInvites
                .AsNoTracking()
                .Where(ci => ci.Token == Token)
                .Select(ci => ci.Course.TenantId)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}