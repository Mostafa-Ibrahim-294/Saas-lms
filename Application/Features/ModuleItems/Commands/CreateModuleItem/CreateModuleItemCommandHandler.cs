using Application.Contracts.Repositories;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ModuleItems.Commands.CreateModuleItem
{
    internal sealed class CreateModuleItemCommandHandler : IRequestHandler<CreateModuleItemCommand, OneOf<SuccessDto, Error>>
    {
        private readonly ITenantMemberRepository _tenantMemberRepository;
        private readonly ICurrentUserId _currentUserId;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IModuleItemRepository _moduleItemRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly IMapper _mapper;
        public CreateModuleItemCommandHandler(ITenantMemberRepository tenantMemberRepository, ICurrentUserId currentUserId,
            ISubscriptionRepository subscriptionRepository, IHttpContextAccessor httpContextAccessor, ICourseRepository courseRepository,
            IMapper mapper, IModuleRepository moduleRepository, IModuleItemRepository moduleItemRepository, ITenantRepository tenantRepository)
        {
            _tenantMemberRepository = tenantMemberRepository;
            _currentUserId = currentUserId;
            _subscriptionRepository = subscriptionRepository;
            _httpContextAccessor = httpContextAccessor;
            _courseRepository = courseRepository;
            _mapper = mapper;
            _moduleRepository = moduleRepository;
            _moduleItemRepository = moduleItemRepository;
            _tenantRepository = tenantRepository;
        }
        public async Task<OneOf<SuccessDto, Error>> Handle(CreateModuleItemCommand request, CancellationToken cancellationToken)
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
            var moduleItem = _mapper.Map<ModuleItem>(request);
            await _tenantRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var moduleItemId = await _moduleItemRepository.CreateModuleItem(moduleItem, cancellationToken);
            if (request.Type == ModuleItemType.Lesson)
            {
                var lesson = _mapper.Map<Lesson>(request, opt =>
                {
                    opt.AfterMap((src, dest) =>
                    {
                        dest.ModuleItemId = moduleItemId;
                    });
                }
                );
                await _moduleItemRepository.CreateLesson(lesson, cancellationToken);
            }
            else
            {
                var assignment = _mapper.Map<Assignment>(request, opt =>
                {
                    opt.AfterMap((src, dest) =>
                    {
                        dest.ModuleItemId = moduleItemId;
                        dest.CreatedById = userId;
                    });
                }
                );
                await _moduleItemRepository.CreateAssignment(assignment, cancellationToken);
            }
                await _tenantRepository.CommitTransactionAsync(cancellationToken);
                return new SuccessDto
                {
                    Id = moduleItemId.ToString(),
                    Message = $"{nameof(ModuleItem)} {SuccessConstatns.ItemCreated}"
                };
            }
            catch
            {
                await _tenantRepository.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
