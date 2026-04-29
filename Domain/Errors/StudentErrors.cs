using System.Net;

namespace Domain.Errors
{
    public static class StudentErrors
    {
        public static Error StudentNotFound =>
            new Error("Student.StudentNotFound", "الطالب المحدد غير موجود.", HttpStatusCode.BadRequest);
        public static Error InvalidToken =>
            new Error("Student.InvalidToken", "الرمز المقدم غير صالح.", HttpStatusCode.BadRequest);

        public static Error AlreadyEnrolled =>
            new Error("Student.AlreadyEnrolled", "الطالب مسجل بالفعل في هذا الكورس.", HttpStatusCode.BadRequest);
    }
}