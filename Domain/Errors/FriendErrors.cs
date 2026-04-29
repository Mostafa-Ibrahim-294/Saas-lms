using System.Net;

namespace Domain.Errors
{
    public static class FriendErrors
    {
        public static Error CannotRequestYourself =>
            new("Friend.CannotRequestYourself", "لا يمكنك إرسال طلب صداقة إلى نفسك", HttpStatusCode.BadRequest);

        public static Error RequestAlreadyExists =>
            new("Friend.RequestAlreadyExists", "تم إرسال طلب صداقة لهذا المستخدم من قبل", HttpStatusCode.Conflict);

        public static Error RequestNotFound =>
            new("Friend.RequestNotFound", "هذا الطلب غير موجود او ليس في حاله انتظار", HttpStatusCode.NotFound);
    }
}