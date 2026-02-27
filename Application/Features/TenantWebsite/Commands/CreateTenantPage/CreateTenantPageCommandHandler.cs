using Application.Contracts.Repositories;
using Application.Features.TenantWebsite.Dtos;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Features.TenantWebsite.Commands.CreateTenantPage
{
    internal sealed class CreateTenantPageCommandHandler : IRequestHandler<CreateTenantPageCommand, TenantPageResponse>
    {
        private readonly ITenantWebsiteRepository _tenantWebsiteRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateTenantPageCommandHandler(ITenantWebsiteRepository tenantWebsiteRepository, ITenantRepository tenantRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _tenantWebsiteRepository = tenantWebsiteRepository;
            _tenantRepository = tenantRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<TenantPageResponse> Handle(CreateTenantPageCommand request, CancellationToken cancellationToken)
        {
            var subDomain = _httpContextAccessor.HttpContext?.Request.Cookies[AuthConstants.SubDomain];
            var tenantId = await _tenantRepository.GetTenantIdAsync(subDomain!, cancellationToken);

            await _tenantWebsiteRepository.CreateTenantPageAsync(request, tenantId, cancellationToken);
            await _tenantWebsiteRepository.SaveAsync(cancellationToken);
            return new TenantPageResponse { Message = TenantWebsiteConstants.Created };
        }
    }
}
