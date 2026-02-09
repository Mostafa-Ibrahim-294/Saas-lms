using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.Errors
{
    public static class CourseErrors
    {
        public static Error CourseNotFound => new(
            "Course.NotFound",
            "هذا الكورس غير موجود",
            HttpStatusCode.NotFound
        );

    }
}
