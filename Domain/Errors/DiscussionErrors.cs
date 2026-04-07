using System.Net;

namespace Domain.Errors
{
    public static class DiscussionErrors
    {
        public static Error DiscussionThreadNotFound =>
            new("DiscussionThread.NotFound", "تعذر العثور على موضوع النقاش المطلوب", HttpStatusCode.NotFound);

        public static Error DiscussionReplyNotFound =>
            new("DiscussionReply.NotFound", "تعذر العثور على رد النقاش المطلوب", HttpStatusCode.NotFound);
    }
}