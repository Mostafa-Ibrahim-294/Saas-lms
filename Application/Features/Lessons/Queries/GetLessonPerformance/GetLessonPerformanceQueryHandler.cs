using Application.Contracts.Repositories;
using Application.Features.Lessons.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Lessons.Queries.GetLessonPerformance
{
    internal sealed class GetLessonPerformanceQueryHandler : IRequestHandler<GetLessonPerformanceQuery, OneOf<LessonPerformanceDto, Error>>
    {

        private readonly IModuleRepository _moduleRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILessonRepository _lessonRepository;
        public GetLessonPerformanceQueryHandler(IHttpContextAccessor httpContextAccessor, ICourseRepository courseRepository,
            IModuleRepository moduleRepository, ILessonRepository lessonRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _courseRepository = courseRepository;
            _moduleRepository = moduleRepository;
            _lessonRepository = lessonRepository;
        }
        public async Task<OneOf<LessonPerformanceDto, Error>> Handle(GetLessonPerformanceQuery request, CancellationToken cancellationToken)
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
            var isLessonFound = await _lessonRepository.IsFound(request.ItemId, cancellationToken);
            if (!isLessonFound)
            {
                return ModuleItemErrors.ModuleItemNotFound;
            }
            var views = await _lessonRepository.GetViewsOverTimeAsync(request.ItemId, cancellationToken);
            return new LessonPerformanceDto
            {
                ViewsOverTime = views
            };
        }
    }
}
