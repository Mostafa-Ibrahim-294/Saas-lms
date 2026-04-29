using System.Net;

namespace Domain.Errors
{
    public static class AnnouncementErrors
    {
        public static Error AnnouncementNotFound
            => new Error("Announcement.NotFound", "لم يتم العثور على الإعلان المطلوب.", HttpStatusCode.BadRequest);
    }
}
