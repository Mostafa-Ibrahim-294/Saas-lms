using Application.Features.StudentUsers.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Infrastructure.Repositories
{
    internal sealed class StudentUserRepository : IStudentUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StudentUserRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<StudentUserProfileDto> GetUserProfileAsync(string userId, string role, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .ProjectTo<StudentUserProfileDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            user!.Role = role;
            return user;
        }
    }
}