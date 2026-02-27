using Application.Features.TenantWebsite.Dtos;
using Domain.Enums;

namespace Application.Features.TenantWebsite.Commands.CreateTenantPage
{
    public sealed record CreateTenantPageCommand(string Title, string Url, TenantPageStatus Status, string? MetaTitle,
        string? MetaDescription, List<PageBlocksDto> PageBlocks) : IRequest<TenantPageResponse>;
}
