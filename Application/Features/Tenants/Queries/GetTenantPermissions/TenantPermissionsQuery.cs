using Application.Features.Tenants.Dtos;

namespace Application.Features.Tenants.Queries.GetTenantPermissions
{
    public sealed record TenantPermissionsQuery : IRequest<List<TenantPermissionDto>>;
}
