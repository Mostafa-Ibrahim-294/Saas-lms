using System.Net;

namespace Domain.Errors
{
    public static class TenantPaymentMethodErrors
    {
        public static Error CreateFailed =>
            new("TenantPaymentMethod.CreateFailed", "فشل إنشاء وسيلة الدفع.", HttpStatusCode.BadRequest);

        public static Error AlreadyExists =>
            new("TenantPaymentMethod.AlreadyExists", "طريقة الدفع هذه مضافة بالفعل.", HttpStatusCode.BadRequest);

        public static Error PaymentMethodNotFound =>
            new("TenantPaymentMethod.NotFound", "طريقة الدفع المطلوبة غير موجودة.", HttpStatusCode.NotFound);

        public static Error DeleteFailed =>
            new("TenantPaymentMethod.DeleteFailed", "فشل حذف وسيلة الدفع.", HttpStatusCode.BadRequest);
    }
}