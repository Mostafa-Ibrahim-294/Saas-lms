using Application.Contracts.Repositories;
using Application.Features.Public.Dtos;

namespace Application.Features.Public.Queries.GetTenantNavigationLinks
{
    internal sealed class GetTenantNavigationLinksQueryHandler : IRequestHandler<GetTenantNavigationLinksQuery, List<TenantNavigationLinkDto>>
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly ITenantPageRepository _tenantPageRepository;

        public GetTenantNavigationLinksQueryHandler(ITenantRepository tenantRepository, ITenantPageRepository tenantPageRepository)
        {
            _tenantRepository = tenantRepository;
            _tenantPageRepository = tenantPageRepository;
        }
        public async Task<List<TenantNavigationLinkDto>> Handle(GetTenantNavigationLinksQuery request, CancellationToken cancellationToken)
        {
            var tenantId = await _tenantRepository.GetTenantIdAsync(request.SubDomain, cancellationToken);
            return await _tenantPageRepository.GetTenantNavigationLinksAsync(tenantId, cancellationToken);
        }
    }
}