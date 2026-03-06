using Application.Features.Public.Dtos;

namespace Application.Features.Public.Queries.GetTenantNavigationLinks
{
    public sealed record GetTenantNavigationLinksQuery(string SubDomain) : IRequest<List<TenantNavigationLinkDto>>;
}
