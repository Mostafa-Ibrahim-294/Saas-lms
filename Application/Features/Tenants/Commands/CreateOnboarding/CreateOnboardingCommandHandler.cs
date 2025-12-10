using Application.Contracts.Repositories;
using Application.Features.Tenants.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Domain.Constants;
using Application.Constants;

namespace Application.Features.Tenants.Commands.CreateOnboarding
{
    internal sealed class CreateOnboardingCommandHandler : IRequestHandler<CreateOnboardingCommand, OneOf<OnboardingDto, Error>>
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        public CreateOnboardingCommandHandler(ITenantRepository tenantRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor
            , UserManager<ApplicationUser> userManager)
        {
            _tenantRepository = tenantRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<OneOf<OnboardingDto, Error>> Handle(CreateOnboardingCommand request, CancellationToken cancellationToken)
        {
            var isSubDomainExists = await _tenantRepository.IsSubDomainExistsAsync(request.SubDomain, cancellationToken);
            if (isSubDomainExists)
            {
                return TenantErrors.SubDomainAlreadyExists;
            }
            var ownerId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(ownerId!);
            var tenant = _mapper.Map<Tenant>(request);
            tenant.OwnerId = ownerId!;
            var createdTenantId =  await _tenantRepository.CreateTenantAsync(tenant, cancellationToken);
            _mapper.Map(request, user!);
            await _tenantRepository.AddTenantRoles(createdTenantId, cancellationToken);
            await _tenantRepository.SaveAsync(cancellationToken);
            var tenantMember = _mapper.Map<TenantMember>(request);
            tenantMember.UserId = ownerId!;
            var tenantRole = await _tenantRepository.FindTenantRoleByTenantId(createdTenantId, RolesConstants.Owner, cancellationToken);
            tenantMember.TenantRole = tenantRole!;
            await _tenantRepository.AddTenantMemberAsync(tenantMember, cancellationToken);
            await _tenantRepository.SaveAsync(cancellationToken);
            _httpContextAccessor?.HttpContext?.Response.Cookies.Append(AuthConstants.SubDomain, request.SubDomain, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });
            return new OnboardingDto
            {
                SubDomain = request.SubDomain
            };
        }
    }
}
