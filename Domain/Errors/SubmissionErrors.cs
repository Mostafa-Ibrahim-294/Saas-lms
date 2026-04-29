using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.Errors
{
    public static class SubmissionErrors
    {
        public static Error SubmissionNotFound => new(
            "Submission.NotFound",
            "التقديم غير موجود",
            HttpStatusCode.NotFound
        );

    }
}
