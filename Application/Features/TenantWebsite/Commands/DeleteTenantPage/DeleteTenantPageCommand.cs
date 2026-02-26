using Application.Features.TenantWebsite.Dtos;

namespace Application.Features.TenantWebsite.Commands.DeleteTenantPage
{
    public sealed record DeleteTenantPageCommand(int PageId) : IRequest<OneOf<TenantPageResponse, Error>>;
}
