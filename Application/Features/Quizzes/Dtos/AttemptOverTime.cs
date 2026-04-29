using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Quizzes.Dtos
{
    public sealed class AttemptOverTime
    {
        public DateTime Date { get; set; }
        public int Attempts { get; set; }
    }
}
