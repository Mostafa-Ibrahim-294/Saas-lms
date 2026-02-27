using Application.Contracts.Repositories;
using Application.Features.ModuleItems.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ModuleItems.Queries.GetSettings
{
    internal sealed class GetSettingsQueryHandler : IRequestHandler<GetSettingsQuery, OneOf<SettingsDto, Error>>
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IModuleItemRepository _moduleItemRepository;
        public GetSettingsQueryHandler(IHttpContextAccessor httpContextAccessor, ICourseRepository courseRepository,
           IModuleRepository moduleRepository, IModuleItemRepository moduleItemRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _courseRepository = courseRepository;
            _moduleRepository = moduleRepository;
            _moduleItemRepository = moduleItemRepository;
        }
        public async Task<OneOf<SettingsDto, Error>> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
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
            var settings = await _moduleItemRepository.GetSettingsAsync(request.ItemId, cancellationToken);
            if (settings is null)
            {
                return ModuleItemErrors.ModuleItemNotFound;
            }
            return settings;
        }
    }
}
