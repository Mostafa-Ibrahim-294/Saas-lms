using Application.Contracts.Repositories;
using Application.Features.Assignments.Dtos;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Assignments.Queries
{
    internal sealed class GetSubmissionsQueryHandler : IRequestHandler<GetSubmissionsQuery, OneOf<IEnumerable<StudentSubmissionDto>, Error>>
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IModuleItemRepository _moduleItemRepository;
        public GetSubmissionsQueryHandler(IHttpContextAccessor httpContextAccessor, ICourseRepository courseRepository,
            IModuleRepository moduleRepository, IAssignmentRepository assignmentRepository, IModuleItemRepository moduleItemRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _courseRepository = courseRepository;
            _moduleRepository = moduleRepository;
            _assignmentRepository = assignmentRepository;
            _moduleItemRepository = moduleItemRepository;
        }
        public async Task<OneOf<IEnumerable<StudentSubmissionDto>, Error>> Handle(GetSubmissionsQuery request, CancellationToken cancellationToken)
        {
            var subdomain = _httpContextAccessor?.HttpContext?.Request.Cookies[AuthConstants.SubDomain];
            var course = await _courseRepository.GetCourseByIdAsync(request.CourseId, subdomain!, cancellationToken);
            if (course is null)
            {
                return CourseErrors.CourseNotFound;
            }
            var module = await _moduleRepository.GetModuleByIdAsync(request.ModuleId, cancellationToken);
            if (module is null)
            {
                return ModuleErrors.ModuleNotFound;
            }
            var assignment = await _moduleItemRepository.GetAssignmentAsync(request.ItemId, cancellationToken);
            if (assignment is null)
            {
                return ModuleItemErrors.ModuleItemNotFound;
            }
            return await _assignmentRepository.GetSubmissionsAsync(request.CourseId, request.ItemId, cancellationToken);
        }
    }
}
