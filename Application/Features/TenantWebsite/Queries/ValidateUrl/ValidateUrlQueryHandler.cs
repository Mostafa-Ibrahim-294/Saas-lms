using Application.Contracts.Repositories;
using Application.Features.TenantWebsite.Dtos;
using Microsoft.AspNetCore.Http;

namespace Application.Features.TenantWebsite.Queries.ValidateUrl
{
    internal sealed class ValidateUrlQueryHandler : IRequestHandler<ValidateUrlQuery, ValidateUrlDto>
    {
        private readonly ITenantWebsiteRepository _tenantWebsiteRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUrlQueryHandler(ITenantWebsiteRepository tenantWebsiteRepository, ITenantRepository tenantRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _tenantWebsiteRepository = tenantWebsiteRepository;
            _tenantRepository = tenantRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ValidateUrlDto> Handle(ValidateUrlQuery request, CancellationToken cancellationToken)
        {
            var subDomain = _httpContextAccessor.HttpContext?.Request.Cookies[AuthConstants.SubDomain];
            var tenantId = await _tenantRepository.GetTenantIdAsync(subDomain!, cancellationToken);
            return new ValidateUrlDto(await _tenantWebsiteRepository.IsValidateUrl(1, request.Url, cancellationToken));
        }
    }
}
