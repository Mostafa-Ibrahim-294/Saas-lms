using System.Net;

namespace Domain.Errors
{
    public static class OrderErrors
    {
        public static Error OrderApproveFailed =>
            new("Order.ApproveFailed", "فشل في الموافقة على الطلب. قد يكون الطلب غير موجود أو ليس في حالة انتظار.", HttpStatusCode.BadRequest);

        public static Error OrderDeclineFailed =>
            new("Order.DeclineFailed", "فشل في رفض الطلب. قد يكون الطلب غير موجود أو ليس في حالة انتظار.", HttpStatusCode.BadRequest);

        public static Error BulkActionFailed =>
            new("Bulk.ActionFailed", "فشل في تنفيذ العملية على الطلبات المحددة. يرجى المحاولة مرة أخرى.", HttpStatusCode.BadRequest);

        public static Error OrderNotFound =>
            new("Order.NotFound", "هذا الطلب غير موجود", HttpStatusCode.NotFound);

        public static Error CanNotUpdatedApprovedOrder =>
            new("Order.UpdatedFailed", "فشل التعديل على الطلب ... لا يمكن التعديل علي طلب بعد قبوله.", HttpStatusCode.BadRequest);

        public static Error CanNotUpdatedDeclinedOrder =>
            new("Order.UpdatedFailed", "فشل التعديل على الطلب ... لا يمكن التعديل علي طلب بعد رفضه.", HttpStatusCode.BadRequest);
    }
}