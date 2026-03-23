using Application.Features.TenantWebsiteSettings.Dtos;

namespace Application.Features.TenantWebsiteSettings.Queries.GetSettings
{
    public sealed record GetTenantWebsiteSettingsQuery : IRequest<OneOf<TenantWebsiteSettingsDto, Error>>;
}
