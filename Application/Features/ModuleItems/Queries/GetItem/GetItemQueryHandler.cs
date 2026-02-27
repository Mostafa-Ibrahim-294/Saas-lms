using Application.Contracts.Repositories;
using Application.Features.ModuleItems.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ModuleItems.Queries.GetItem
{
    internal sealed class GetItemQueryHandler : IRequestHandler<GetItemQuery, OneOf<ItemDto, Error>>
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IModuleItemRepository _moduleItemRepository;
        private readonly IMapper _mapper;
        public GetItemQueryHandler(IHttpContextAccessor httpContextAccessor, ICourseRepository courseRepository,
            IMapper mapper, IModuleRepository moduleRepository, IModuleItemRepository moduleItemRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _courseRepository = courseRepository;
            _mapper = mapper;
            _moduleRepository = moduleRepository;
            _moduleItemRepository = moduleItemRepository;
        }
        public async Task<OneOf<ItemDto, Error>> Handle(GetItemQuery request, CancellationToken cancellationToken)
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
            var moduleItem = await _moduleItemRepository.GetAsync(request.ItemId, cancellationToken);
            if (moduleItem is null)
            {
                return ModuleItemErrors.ModuleItemNotFound;
            }
            return _mapper.Map<ItemDto>(moduleItem);
        }
    }
}
