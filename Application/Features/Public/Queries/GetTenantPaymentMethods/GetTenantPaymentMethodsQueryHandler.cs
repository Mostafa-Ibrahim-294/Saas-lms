using Application.Contracts.Repositories;
using Application.Features.TenantPaymentMethods.Dtos;

namespace Application.Features.Public.Queries.GetTenantPaymentMethods
{
    internal sealed class GetTenantPaymentMethodsQueryHandler : IRequestHandler<GetTenantPaymentMethodsQuery, List<PaymentMethodDto>>
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly ITenantRepository _tenantRepository;

        public GetTenantPaymentMethodsQueryHandler(IPaymentMethodRepository paymentMethodRepository, ITenantRepository tenantRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _tenantRepository = tenantRepository;
        }
        public async Task<List<PaymentMethodDto>> Handle(GetTenantPaymentMethodsQuery request, CancellationToken cancellationToken)
        {
            var tenantId = await _tenantRepository.GetTenantIdAsync(request.SubDomain, cancellationToken);
            return await _paymentMethodRepository.GetPaymentMethodsByTenantIdAsync(tenantId, cancellationToken);
        }
    }
}