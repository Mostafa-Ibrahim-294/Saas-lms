using Application.Features.TenantWebsite.Dtos;

namespace Application.Features.TenantWebsite.Queries.GetTenantPage
{
    public sealed record GetTenantPageQuery(int PageId) : IRequest<OneOf<TenantPageDto, Error>>;
}
