using Application.Contracts.Repositories;
using Application.Features.TenantWebsite.Dtos;
using Microsoft.AspNetCore.Http;

namespace Application.Features.TenantWebsite.Commands.DuplicateTenantPage
{
    internal sealed class DuplicateTenantPageCommandHandler : IRequestHandler<DuplicateTenantPageCommand, OneOf<TenantPageResponse, Error>>
    {
        private readonly ITenantWebsiteRepository _tenantWebsiteRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DuplicateTenantPageCommandHandler(ITenantWebsiteRepository tenantWebsiteRepository, ITenantRepository tenantRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _tenantWebsiteRepository = tenantWebsiteRepository;
            _tenantRepository = tenantRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<OneOf<TenantPageResponse, Error>> Handle(DuplicateTenantPageCommand request, CancellationToken cancellationToken)
        {
            var subDomain = _httpContextAccessor.HttpContext?.Request.Cookies[AuthConstants.SubDomain];
            var tenantId = await _tenantRepository.GetTenantIdAsync(subDomain!, cancellationToken);

            var tenantPage = await _tenantWebsiteRepository.GetTenantPageAsync(tenantId, request.PageId, cancellationToken);
            if(tenantPage is null)
                return TenantWebsiteErrors.TenantPageNotFound;

            var duplicatedTenantPage = new TenantPage
            {
                Title = tenantPage.Title ,
                MetaTitle = tenantPage.MetaTitle,
                MetaDescription = tenantPage.MetaDescription,
                Status = tenantPage.Status,
                IsHomePage = tenantPage.IsHomePage,
                TenantId = tenantPage.TenantId,
                PageBlocks = tenantPage.PageBlocks.Select(pb => new PageBlock
                {
                    BlockTypeId = pb.BlockTypeId,
                    Order = pb.Order,
                    Visible = pb.Visible,
                    Props = pb.Props,
                }).ToList()
            };
            await _tenantWebsiteRepository.DuplicateTenantPageAsync(duplicatedTenantPage,cancellationToken);
            await _tenantWebsiteRepository.SaveAsync(cancellationToken);
            duplicatedTenantPage.Url = $"{tenantPage.Url}-{duplicatedTenantPage.Id}";
            await _tenantWebsiteRepository.SaveAsync(cancellationToken);
            return new TenantPageResponse { Message = TenantWebsiteConstants.Duplicated };
        }
    }
}
