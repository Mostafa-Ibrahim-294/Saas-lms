using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Quizzes.Dtos
{
    public sealed class PerformanceDto
    {
        public IEnumerable<AttemptOverTime> AttemptsOverTime { get; set; } = null!;
        public IEnumerable<GradeDistribution> GradesDistribution { get; set; } = null!;
        public IEnumerable<MostDifficultQuestion> MostDifficultQuestions { get; set; } = null!;
    }
}
