using Application.Features.Public.Dtos;

namespace Application.Features.Public.Queries.GetTenantNavigationLinks
{
    public sealed record GetTenantNavigationLinksQuery : IRequest<List<TenantNavigationLinkDto>>;
}
