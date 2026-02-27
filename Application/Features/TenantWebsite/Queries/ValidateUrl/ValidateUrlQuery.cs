using Application.Features.TenantWebsite.Dtos;

namespace Application.Features.TenantWebsite.Queries.ValidateUrl
{
    public sealed record ValidateUrlQuery(string Url) : IRequest<ValidateUrlDto>;
}
