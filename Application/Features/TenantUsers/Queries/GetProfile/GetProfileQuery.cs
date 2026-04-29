using Application.Features.TenantUsers.Dtos;

namespace Application.Features.TenantUsers.Queries.GetProfile
{
    public sealed record GetProfileQuery : IRequest<TenantUserProfileDto>;
}
