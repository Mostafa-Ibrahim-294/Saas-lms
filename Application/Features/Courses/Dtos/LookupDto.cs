using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Courses.Dtos
{
    public sealed class LookupDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
