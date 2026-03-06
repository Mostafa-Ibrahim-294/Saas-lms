using Application.Features.TenantWebsiteSettings.Dtos;

namespace Application.Features.Public.Queries.GetTenantSettings
{
    public sealed record GetTenantSettingsQuery(string SubDomain) : IRequest<TenantWebsiteSettingsDto>;
}
