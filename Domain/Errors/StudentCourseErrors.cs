using System.Net;

namespace Domain.Errors
{
    public static class StudentCourseErrors
    {
        public static Error StudentNotEnrolledInCourse =>
            new Error("StudentCourse.StudentNotEnrolledInCourse", "الطالب غير مشترك في هذا الكورس.", HttpStatusCode.BadRequest);
    }
}