using Application.Contracts.Repositories;
using Application.Features.TenantWebsite.Dtos;

namespace Application.Features.TenantWebsite.Queries.GetTenantPageBlocks
{
    internal sealed class GetTenantPageBlocksQueryHandler : IRequestHandler<GetTenantPageBlocksQuery, TenantPageBlocksDto>
    {
        private readonly ITenantWebsiteRepository _tenantWebsiteRepository;
        public GetTenantPageBlocksQueryHandler(ITenantWebsiteRepository tenantWebsiteRepository)
        {
            _tenantWebsiteRepository = tenantWebsiteRepository;
        }
        public async Task<TenantPageBlocksDto> Handle(GetTenantPageBlocksQuery request, CancellationToken cancellationToken)
        {
            var tenantPageBlocks = await _tenantWebsiteRepository.GetBlocksTypeAsync(cancellationToken);
            return tenantPageBlocks!;
        }
    }
}
