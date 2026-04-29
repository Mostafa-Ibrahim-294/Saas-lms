using Application.Features.TenantUsers.Dtos;

namespace Application.Features.TenantUsers.Queries.GetTenants
{
    public sealed record GetTenantsQuery : IRequest<IEnumerable<UserTenantsDto>>;
}
