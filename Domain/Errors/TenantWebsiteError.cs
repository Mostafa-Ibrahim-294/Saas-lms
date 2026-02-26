using System.Net;

namespace Domain.Errors
{
    public sealed class TenantWebsiteError
    {
        public static Error TenantPageNotFound =>
            new Error( "TenantPage.NotFound", "صفحة المستأجر غير موجودة", HttpStatusCode.NotFound);
    }
}
