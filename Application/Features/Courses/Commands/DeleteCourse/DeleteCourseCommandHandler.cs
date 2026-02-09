using Application.Contracts.Repositories;
using Application.Features.Courses.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Courses.Commands.DeleteCourse
{
    internal sealed class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, OneOf<CourseDto, Error>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ITenantMemberRepository _tenantMemberRepository;
        private readonly ICurrentUserId _currentUserId;
        public DeleteCourseCommandHandler(ICourseRepository courseRepository, ITenantMemberRepository tenantMemberRepository, ICurrentUserId currentUserId)
        {
            _courseRepository = courseRepository;
            _tenantMemberRepository = tenantMemberRepository;
            _currentUserId = currentUserId;
        }

        public async Task<OneOf<CourseDto, Error>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserId.GetUserId();
            var isPermitted = await _tenantMemberRepository.IsPermittedMember(userId, PermissionConstants.DELETE_COURSES, cancellationToken);
            if (!isPermitted)
            {
                return MemberErrors.NotAllowed;
            }
            var course = await _courseRepository.GetCourseByIdAsync(request.CourseId, cancellationToken);
            if (course == null)
            {
                return CourseErrors.CourseNotFound;
            }
            await _courseRepository.RemoveCourseAsync(course, cancellationToken);
            return new CourseDto
            {
                Id = course.Id.ToString(),
                Message = SuccessConstatns.CourseDeleted
            };
        }
    }
}
