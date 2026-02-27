using Application.Contracts.Repositories;
using Application.Features.TenantOrders.Dtos;
using Microsoft.AspNetCore.Http;

namespace Application.Features.TenantOrders.Queries.GetTenantOrders
{
    internal sealed class GetTenantOrdersQueryHandler : IRequestHandler<GetTenantOrdersQuery, List<TenantOrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetTenantOrdersQueryHandler(IOrderRepository orderRepository, ITenantRepository tenantRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _orderRepository = orderRepository;
            _tenantRepository = tenantRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<TenantOrderDto>> Handle(GetTenantOrdersQuery request, CancellationToken cancellationToken)
        {
            var subDomain = _httpContextAccessor.HttpContext?.Request.Cookies[AuthConstants.SubDomain];
            var tenantId = await _tenantRepository.GetTenantIdAsync(subDomain!, cancellationToken);
            return await _orderRepository.GetTenantOrdersAsync(tenantId, cancellationToken);
        }
    }
}
