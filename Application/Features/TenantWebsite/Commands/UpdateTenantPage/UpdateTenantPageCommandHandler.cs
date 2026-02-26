using Application.Contracts.Repositories;
using Application.Features.TenantWebsite.Dtos;
using Microsoft.AspNetCore.Http;

namespace Application.Features.TenantWebsite.Commands.UpdateTenantPage
{
    internal sealed class UpdateTenantPageCommandHandler : IRequestHandler<UpdateTenantPageCommand, OneOf<TenantPageResponse, Error>>
    {
        private readonly ITenantWebsiteRepository _tenantWebsiteRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateTenantPageCommandHandler(ITenantWebsiteRepository tenantWebsiteRepository, ITenantRepository tenantRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _tenantWebsiteRepository = tenantWebsiteRepository;
            _tenantRepository = tenantRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<OneOf<TenantPageResponse, Error>> Handle(UpdateTenantPageCommand request, CancellationToken cancellationToken)
        {
            var subDomain = _httpContextAccessor.HttpContext?.Request.Cookies[AuthConstants.SubDomain];
            var tenantId = await _tenantRepository.GetTenantIdAsync(subDomain!, cancellationToken);

            var isUpdated = await _tenantWebsiteRepository.UpdateTenantPageAsync(request.pageId, tenantId, request, cancellationToken);
            if (!isUpdated)
                return TenantWebsiteError.TenantPageNotFound;

            return new TenantPageResponse { Message = TenantWebsiteConstants.Updated };
        }
    }
}
