using System.Net;

namespace Domain.Errors
{
    public static class DiscussionErrors
    {
        public static Error DiscussionThreadNotFound =>
            new("DiscussionThread.NotFound", "تعذر العثور على موضوع النقاش المطلوب", HttpStatusCode.NotFound);

        public static Error DiscussionReplyNotFound =>
            new("DiscussionReply.NotFound", "تعذر العثور على رد النقاش المطلوب", HttpStatusCode.NotFound);

        public static Error NotDiscussionOwner =>
            new("Discussion.NotOwner", "لا يمكنك تعديل هذا النقاش لأنه لا يخصك", HttpStatusCode.Forbidden);

        public static Error NotDiscussionReplyOwner =>
            new("Discussion.NotOwner", "لا يمكنك تعديل هذا الرد لأنه لا يخصك", HttpStatusCode.Forbidden);
    }
}