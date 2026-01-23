using Application.Common;
using Application.Features.Users.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Application.Features.Users.Queries.GetProfile
{
    internal sealed class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, UserProfileDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserId _currentUserId;
        private readonly IdentityUserRole<string> _userRole;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public GetProfileQueryHandler(UserManager<ApplicationUser> userManager, ICurrentUserId currentUserId, 
            IdentityUserRole<string> userRole,
            RoleManager<IdentityRole> roleManager
            , IMapper mapper)
        {
            _userManager = userManager;
            _currentUserId = currentUserId;
            _userRole = userRole;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<UserProfileDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserId.GetUserId();
            var user = await _userManager.FindByIdAsync(userId!);

            var roleId= _userRole.UserId;

            var role = await _roleManager.FindByIdAsync(roleId);
            var userProfileDto = _mapper.Map<UserProfileDto>(user);
            userProfileDto.Role = role?.Name ?? string.Empty;
            return userProfileDto;
        }
    }
}
