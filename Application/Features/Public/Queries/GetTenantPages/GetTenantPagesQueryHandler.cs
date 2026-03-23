using Application.Contracts.Repositories;
using Application.Features.TenantWebsite.Dtos;

namespace Application.Features.Public.Queries.GetTenantPages
{
    internal sealed class GetTenantPagesQueryHandler : IRequestHandler<GetTenantPagesQuery, OneOf<TenantPageDto, Error>>
    {
        private readonly ITenantPageRepository _tenantPageRepository;

        public GetTenantPagesQueryHandler(ITenantPageRepository tenantPageRepository)
        {
            _tenantPageRepository = tenantPageRepository;
        }
        public async Task<OneOf<TenantPageDto, Error>> Handle(GetTenantPagesQuery request, CancellationToken cancellationToken)
        {
            var tenantPage = await _tenantPageRepository.GetPublishedTenantPagesAsync(request.Url, request.SubDomain, cancellationToken);
            if (tenantPage is null)
                return TenantWebsiteErrors.TenantPageNotFound;
            return tenantPage;
        }
    }
}
