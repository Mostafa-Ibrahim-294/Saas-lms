using Application.Contracts.Repositories;
using Application.Features.TenantWebsiteSettings.Dtos;

namespace Application.Features.Public.Queries.GetTenantSettings
{
    internal sealed class GetTenantSettingsQueryHandler : IRequestHandler<GetTenantSettingsQuery, TenantWebsiteSettingsDto>
    {
        private readonly ITenantWebsiteSettingsRepository _tenantWebsiteSettingsRepository;
        private readonly ITenantRepository _tenantRepository;

        public GetTenantSettingsQueryHandler(ITenantWebsiteSettingsRepository tenantWebsiteSettingsRepository, ITenantRepository tenantRepository)
        {
            _tenantWebsiteSettingsRepository = tenantWebsiteSettingsRepository;
            _tenantRepository = tenantRepository;
        }
        public async Task<TenantWebsiteSettingsDto> Handle(GetTenantSettingsQuery request, CancellationToken cancellationToken)
        {
            var tenantId = await _tenantRepository.GetTenantIdAsync(request.SubDomain, cancellationToken);
            return await _tenantWebsiteSettingsRepository.GetSettingsAsync(tenantId, cancellationToken);
        }
    }
}