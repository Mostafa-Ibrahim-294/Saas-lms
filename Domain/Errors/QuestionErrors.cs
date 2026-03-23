using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.Errors
{
    public static class QuestionErrors
    {
        public static Error CategoryAlreadyExists => new(
            "Question.CategoryAlreadyExists",
            "هذا التصنيف موجود بالفعل",
            HttpStatusCode.Conflict
        );
        public static Error QuestionNotFound => new(
            "Question.NotFound",
            "هذا السؤال غير موجود",
            HttpStatusCode.NotFound
        );
        public static Error CategoryNotFound => new(
            "Question.CategoryNotFound",
            "هذا التصنيف غير موجود",
            HttpStatusCode.NotFound
        );


    }
}
