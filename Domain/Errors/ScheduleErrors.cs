using System.Net;

namespace Domain.Errors
{
    public static class ScheduleErrors
    {
        public static Error ScheduleConflict =>
            new Error("Schedule.Conflict", "يوجد جدول بالفعل في هذا الوقت.", HttpStatusCode.Conflict);

        public static Error ScheduleNotFound =>
            new Error("Schedule.NotFound", "لم يتم العثور على الجدول المحدد.", HttpStatusCode.NotFound);
    }
}