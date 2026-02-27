using Application.Features.TenantWebsite.Dtos;

namespace Application.Features.TenantWebsite.Queries.GetTenantPageBlocks
{
    public sealed record GetTenantPageBlocksQuery : IRequest<TenantPageBlocksDto>;
}
