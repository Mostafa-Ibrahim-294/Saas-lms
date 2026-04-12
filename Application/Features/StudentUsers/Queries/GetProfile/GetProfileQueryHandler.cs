using Application.Contracts.Repositories;
using Application.Features.StudentUsers.Dtos;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Application.Features.StudentUsers.Queries.GetProfile
{
    internal sealed class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, OneOf<StudentUserProfileDto, Error>>
    {
        private readonly HybridCache _hybridCache;
        private readonly IStudentUserRepository _studentUserRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetProfileQueryHandler(HybridCache hybridCache, IStudentUserRepository studentUserRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _hybridCache = hybridCache;
            _studentUserRepository = studentUserRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<OneOf<StudentUserProfileDto, Error>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var sessionId = _httpContextAccessor.HttpContext?.Request.Cookies[AuthConstants.SessionId];
            var cachedSessionKey = $"{CacheKeysConstants.SessionKey}_{sessionId}";

            var sessionData = await _hybridCache.GetOrCreateAsync(cachedSessionKey, async entry =>
            {
                return await Task.FromResult<string?>(null);
            }, cancellationToken: cancellationToken);

            if (string.IsNullOrEmpty(sessionData))
                return UserErrors.Unauthorized;

            var session = JsonSerializer.Deserialize<UserSession>(sessionData);
            if (session is null)
                return UserErrors.Unauthorized;

            return await _studentUserRepository.GetUserProfileAsync(session.UserId, session.Role, cancellationToken);
        }
    }
}