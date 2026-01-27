using System.Net;

namespace Domain.Errors
{
    public static class FileError
    {
        public static Error UploadFailed =>
            new Error("UploadFailed", "فشل رفع الملف على خادم التخزين", HttpStatusCode.BadRequest);
    }
}
