using System.Net;

namespace Domain.Errors
{
    public static class TenantWebsiteSettingsErrors
    {
        public static Error TenantWebsiteSettingsUpdateFailed =>
            new Error("TenantWebsiteSettingsUpdate.Failed", "فشل في تحديث إعدادات موقع المستأجر.", HttpStatusCode.BadRequest);
    }
}
