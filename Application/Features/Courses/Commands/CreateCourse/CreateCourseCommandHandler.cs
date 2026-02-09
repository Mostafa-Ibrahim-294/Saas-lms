using Application.Common;
using Application.Constants;
using Application.Contracts.Repositories;
using Application.Features.Courses.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Courses.Commands.CreateCourse
{
    internal sealed class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, OneOf<CourseDto, Error>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ITenantMemberRepository _tenantMemberRepository;
        private readonly ICurrentUserId _currentUserId;
        private readonly ITenantRepository _tenantRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public CreateCourseCommandHandler(ICourseRepository courseRepository, ICurrentUserId currentUserId, IMapper mapper,
            ITenantMemberRepository tenantMemberRepository, ITenantRepository tenantRepository, IHttpContextAccessor httpContextAccessor)
        {
            _courseRepository = courseRepository;
            _currentUserId = currentUserId;
            _mapper = mapper;
            _tenantMemberRepository = tenantMemberRepository;
            _tenantRepository = tenantRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OneOf<CourseDto, Error>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserId.GetUserId();
            var isPermitted = await _tenantMemberRepository.IsPermittedMember(userId, PermissionConstants.CREATE_COURSES, cancellationToken);
            if (!isPermitted)
            {
                return MemberErrors.NotAllowed;
            }
            var subDomain = _httpContextAccessor?.HttpContext?.Request.Cookies[AuthConstants.SubDomain];
            var tenantId = await _tenantRepository.GetTenantIdAsync(subDomain!, cancellationToken);
            var isFeatureEnded = await _tenantRepository.IsFeatureUsingEnded(subDomain!, FeatureConstants.COURSE_LIMIT, cancellationToken);
            if (isFeatureEnded)
            {
                return TenantErrors.FeatureUsageEnded;
            }
            var course = _mapper.Map<Course>(request,
                opt => opt.AfterMap((src, dest) =>
                {
                    dest.CreatedById = userId;
                    dest.TenantId = tenantId;
                })
                );
            var courseId = await _courseRepository.CreateCourse(course, cancellationToken);
            await _tenantRepository.IncreasePlanFeatureUsageByKeyAsync(subDomain!, FeatureConstants.COURSE_LIMIT, 1, cancellationToken);
            return new CourseDto
            {
                Id = courseId.ToString(),
                Message = SuccessConstatns.CourseCreated
            };
        }
    }
}
