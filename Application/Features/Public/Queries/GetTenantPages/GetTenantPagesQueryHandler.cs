using Application.Contracts.Repositories;
using Application.Features.TenantWebsite.Dtos;
using Microsoft.Extensions.Logging;

namespace Application.Features.Public.Queries.GetTenantPages
{
    internal sealed class GetTenantPagesQueryHandler : IRequestHandler<GetTenantPagesQuery, OneOf<TenantPageDto, Error>>
    {
        private readonly ITenantPageRepository _tenantPageRepository;
        private readonly ILogger<GetTenantPagesQueryHandler> logger;

        public GetTenantPagesQueryHandler(ITenantPageRepository tenantPageRepository, ILogger<GetTenantPagesQueryHandler> logger)
        {
            _tenantPageRepository = tenantPageRepository;
            this.logger = logger;
        }
        public async Task<OneOf<TenantPageDto, Error>> Handle(GetTenantPagesQuery request, CancellationToken cancellationToken)
        {
            logger.LogWarning("Fetching tenant page for Url: {Url}, SubDomain: {SubDomain}", request.Url, request.SubDomain);
            var tenantPage = await _tenantPageRepository.GetPublishedTenantPagesAsync(request.Url, request.SubDomain, cancellationToken);
            if (tenantPage is null)
            {
                logger.LogWarning("Tenant page not found for Url: {Url}, SubDomain: {SubDomain}", request.Url, request.SubDomain);
                return TenantWebsiteErrors.TenantPageNotFound;
            }
            return tenantPage;
        }
    }
}
