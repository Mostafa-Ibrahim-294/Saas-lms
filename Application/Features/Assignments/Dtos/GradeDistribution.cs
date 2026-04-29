using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Assignments.Dtos
{
    public sealed class GradeDistribution
    {
        public string Range { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
