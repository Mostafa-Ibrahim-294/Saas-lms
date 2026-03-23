using Application.Features.TenantPaymentMethods.Dtos;
using Domain.Enums;
using System.Text.Json;

namespace Application.Contracts.Repositories
{
    public interface IPaymentMethodRepository
    {
        Task<List<PaymentMethodDto>> GetPaymentMethodsAsync(CancellationToken cancellationToken);
        Task<List<PaymentMethodDto>> GetPaymentMethodsByTenantIdAsync(int tenantId, CancellationToken cancellationToken);
        Task<bool> IsPaymentMethodTypeExistAsync(int tenantId, PaymentMethodType type, CancellationToken cancellationToken);
        Task CreatePaymentMethodAsync(PaymentMethod paymentMethod, CancellationToken cancellationToken);
        Task<PaymentMethodDto?> UpdatePaymentMethodAsync(int PaymentMethodId, Dictionary<string, JsonElement> Details, CancellationToken cancellationToken);
        Task<PaymentMethodDto?> UpdatePaymentMethodStatusAsync(int PaymentMethodId, bool IsActive, CancellationToken cancellationToken);
        Task<bool> DeletePayMentMethodAsync(int PaymentMethodId, CancellationToken cancellationToken);
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
