using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Quizzes.Dtos
{
    public sealed class GradeDistribution
    {
        public string Range { get; set; }
        public int Count { get; set; }
    }
}
