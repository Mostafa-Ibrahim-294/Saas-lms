using Application.Features.Friends.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enums;

namespace Infrastructure.Repositories
{
    internal sealed class FriendRepository : IFriendRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FriendRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateRequestAsync(Friend friend, CancellationToken cancellationToken)
        {
            await _context.Friends.AddAsync(friend, cancellationToken);
        }
        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<List<FriendsDto>> GetFriendsAsync(int studentId, CancellationToken cancellationToken)
        {
            return await _context.Friends
                .AsNoTracking()
                .Where(f => f.Status == FriendStatus.Accepted && (f.Student1Id == studentId || f.Student2Id == studentId))
                .ProjectTo<FriendsDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
        public async Task<RequestsDto> GetRequestsAsync(int studentId, CancellationToken cancellationToken)
        {
            var incomingRequests = await _context.Friends
                .AsNoTracking()
                .Where(f => f.Status == FriendStatus.Pending
                    && f.ActionStudentId != studentId
                    && (f.Student1Id == studentId || f.Student2Id == studentId))
                .ProjectTo<FriendRequestDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var SentRequests = await _context.Friends
                .AsNoTracking()
                .Where(f => f.Status == FriendStatus.Pending && f.ActionStudentId == studentId)
                .ProjectTo<FriendRequestDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new RequestsDto
            {
                FriendRequests = incomingRequests,
                SentRequests = SentRequests
            };
        }
        public async Task<bool> RequestAlreadySentAsync(int studentSenderId, int studentRecevierId, CancellationToken cancellationToken)
        {
            var s1 = Math.Min(studentSenderId, studentRecevierId);
            var s2 = Math.Max(studentSenderId, studentRecevierId);
            return await _context.Friends.AnyAsync(f => f.Student1Id == s1 && f.Student2Id == s2, cancellationToken);
        }
        public async Task<Friend?> GetFriendRequestPendingAsync(int requestId, CancellationToken cancellationToken)
        {
            return await _context.Friends.FirstOrDefaultAsync(f => f.Id == requestId && f.Status == FriendStatus.Pending, cancellationToken);
        }
        public async Task UpdateRequestStatusAsync(Friend request, CancellationToken cancellationToken)
        {
            _context.Friends.Update(request);
        }
    }
}