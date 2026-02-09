using Application.Contracts.Repositories;
using Application.Features.Courses.Dtos;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Courses.Commands.UpdateCourse
{
    internal sealed class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, OneOf<CourseDto, Error>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private readonly ITenantMemberRepository _tenantMemberRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ICurrentUserId _currentUserId;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateCourseCommandHandler(
            ICourseRepository courseRepository,
            IMapper mapper,
            ITenantMemberRepository tenantMemberRepository,
            ISubscriptionRepository subscriptionRepository,
            ICurrentUserId currentUserId,
            IHttpContextAccessor httpContextAccessor)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _tenantMemberRepository = tenantMemberRepository;
            _subscriptionRepository = subscriptionRepository;
            _currentUserId = currentUserId;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OneOf<CourseDto, Error>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserId.GetUserId();
            var isPermitted = await _tenantMemberRepository.IsPermittedMember(userId, PermissionConstants.EDIT_COURSES, cancellationToken);
            if (!isPermitted)
            {
                return MemberErrors.NotAllowed;
            }
            var subDomain = _httpContextAccessor?.HttpContext?.Request.Cookies[AuthConstants.SubDomain];
            var isSubscribed = await _subscriptionRepository.HasActiveSubscriptionByTenantDomain(subDomain!, cancellationToken);
            if (!isSubscribed)
            {
                return TenantErrors.NotSubscribed;
            }
            var course = await _courseRepository.GetCourseByIdAsync(request.CourseId, cancellationToken);
            if (course == null)
            {
                return CourseErrors.CourseNotFound;
            }
            _mapper.Map(request, course);
            await _courseRepository.SaveAsync(cancellationToken);
            return new CourseDto
            {
                Id = course.Id.ToString(),
                Message = SuccessConstatns.CourseUpdated
            };
        }
    }
}
