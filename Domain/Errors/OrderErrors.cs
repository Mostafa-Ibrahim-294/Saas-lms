using System.Net;

namespace Domain.Errors
{
    public static class OrderErrors
    {
        public static Error OrderApproveFailed => 
            new Error("Order.ApproveFailed", "فشل في الموافقة على الطلب. قد يكون الطلب غير موجود أو ليس في حالة انتظار.", HttpStatusCode.BadRequest);

        public static Error OrderDeclineFailed =>
            new Error("Order.DeclineFailed", "فشل في رفض الطلب. قد يكون الطلب غير موجود أو ليس في حالة انتظار.", HttpStatusCode.BadRequest);

        public static Error BulkActionFailed =>
            new("Bulk.ActionFailed", "فشل في تنفيذ العملية على الطلبات المحددة. يرجى المحاولة مرة أخرى.",HttpStatusCode.BadRequest);
    }
}
