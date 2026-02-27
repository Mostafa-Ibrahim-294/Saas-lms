using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.Errors
{
    public static class ModuleItemErrors
    {
        public static Error ModuleItemNotFound => new(
            "ModuleItem.NotFound",
            "هذا العنصر غير موجود",
            HttpStatusCode.NotFound
        );

    }
}
