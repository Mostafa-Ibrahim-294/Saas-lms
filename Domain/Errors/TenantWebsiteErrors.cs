using System.Net;

namespace Domain.Errors
{
    public sealed class TenantWebsiteErrors
    {
        public static Error TenantPageNotFound =>
            new Error( "TenantPage.NotFound", "صفحة المستأجر غير موجودة", HttpStatusCode.NotFound);
    }
}
