using Application.Features.TenantWebsite.Dtos;

namespace Application.Features.Public.Queries.GetTenantPages
{
    public sealed record GetTenantPagesQuery(string Url, string SubDomain) : IRequest<OneOf<TenantPageDto, Error>>;
}
