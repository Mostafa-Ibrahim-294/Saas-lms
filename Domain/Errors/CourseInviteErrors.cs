using System.Net;

namespace Domain.Errors
{
    public static class CourseInviteErrors
    {
        public static Error InviteExpired =>
            new Error("Course.Expired", "انتهت صلاحية الدعوة", HttpStatusCode.Gone);

        public static Error InviteError =>
           new Error("Course.Error", "حدث خطأ اثناء قبول الدعوة", HttpStatusCode.Conflict);

        public static Error AlreadyInvited =>
            new Error("Course.AlreadyInvited", "تمت دعوة هذا الطالب بالفعل لهذا المقرر", HttpStatusCode.Conflict);
    }
}