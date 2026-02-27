using Application.Contracts.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Features.ModuleItems.Commands.UpdateLesson
{
    internal sealed class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand, OneOf<SuccessDto, Error>>
    {
        private readonly ITenantMemberRepository _tenantMemberRepository;
        private readonly ICurrentUserId _currentUserId;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IModuleItemRepository _moduleItemRepository;
        private readonly IMapper _mapper;
        public UpdateLessonCommandHandler(ITenantMemberRepository tenantMemberRepository, ICurrentUserId currentUserId,
            ISubscriptionRepository subscriptionRepository, IHttpContextAccessor httpContextAccessor, ICourseRepository courseRepository,
            IMapper mapper, IModuleRepository moduleRepository, IModuleItemRepository moduleItemRepository)
        {
            _tenantMemberRepository = tenantMemberRepository;
            _currentUserId = currentUserId;
            _subscriptionRepository = subscriptionRepository;
            _httpContextAccessor = httpContextAccessor;
            _courseRepository = courseRepository;
            _mapper = mapper;
            _moduleRepository = moduleRepository;
            _moduleItemRepository = moduleItemRepository;
        }
        public async Task<OneOf<SuccessDto, Error>> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserId.GetUserId();
            var subdomain = _httpContextAccessor?.HttpContext?.Request.Cookies[AuthConstants.SubDomain];
            var isPermitted = await _tenantMemberRepository.IsPermittedMember(userId, PermissionConstants.MANAGE_MODULE_ITEMS, cancellationToken);
            if (!isPermitted)
            {
                return MemberErrors.NotAllowed;
            }
            var isSubscribed = await _subscriptionRepository.HasActiveSubscriptionByTenantDomain(subdomain!, cancellationToken);
            if (!isSubscribed)
            {
                return TenantErrors.NotSubscribed;
            }
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
            var lesson = await _moduleItemRepository.GetLessonByModuleItemIdAsync(request.ItemId, cancellationToken);
            if (lesson is null)
            {
                return ModuleItemErrors.ModuleItemNotFound;
            }
            _mapper.Map(request, lesson);
            await _courseRepository.SaveAsync(cancellationToken);
            return new SuccessDto
            {
                Id = lesson.ModuleItemId.ToString(),
                Message = $"{nameof(ModuleItem)} {SuccessConstatns.ItemUpdated}"
            };
        }
    }
}
