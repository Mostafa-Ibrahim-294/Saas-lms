using Application.Contracts.Repositories;
using Application.Features.TenantStudents.Dtos;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Application.Features.StudentUsers.Commands.Onboarding
{
    internal sealed class OnboardingCommandHandler : IRequestHandler<OnboardingCommand, OneOf<StudentResponse, Error>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly HybridCache _hybridCache;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStudentSubjectRepository _studentSubjectRepository;
        private readonly ITenantRepository _tenantRepository;

        public OnboardingCommandHandler(IStudentRepository studentRepository, HybridCache hybridCache, IMapper mapper,
            IHttpContextAccessor httpContextAccessor, IStudentSubjectRepository studentSubjectRepository, ITenantRepository tenantRepository)
        {
            _studentRepository = studentRepository;
            _hybridCache = hybridCache;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _studentSubjectRepository = studentSubjectRepository;
            _tenantRepository = tenantRepository;
        }
        public async Task<OneOf<StudentResponse, Error>> Handle(OnboardingCommand request, CancellationToken cancellationToken)
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

            var student = await _studentRepository.GetStudentAsync(session.StudentId, cancellationToken);
            if (student is null)
                return UserErrors.Unauthorized;

            var studentUserId = await _studentRepository.GetStudentUserIdAsync(session.StudentId, cancellationToken);

            await _tenantRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                _mapper.Map(request, student);
                
                await _studentRepository.UpdateHasOnboardedAsync(studentUserId, cancellationToken);
                var subjectIds = await _studentSubjectRepository.GetSubjectIdsAsync(request.Subjects, cancellationToken);
                var newStudentSubjects = subjectIds.Select(kvp => new StudentSubject
                {
                    Confidence = request.Confidence[kvp.Key],
                    StudentId = session.StudentId,
                    AvailableSubjectId = kvp.Value
                }).ToList();

                await _studentSubjectRepository.CreateStudentSubjectAsync(newStudentSubjects, cancellationToken);
                await _tenantRepository.CommitTransactionAsync(cancellationToken);
                return new StudentResponse { Message = MessagesConstants.StudentOnboardingCompleted };
            }
            catch
            {
                await _tenantRepository.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}