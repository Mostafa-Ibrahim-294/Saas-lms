using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ModuleItems.Dtos
{
    public sealed class QuizDto
    {
        public int Duration { get; set; }
        public int PassingScore { get; set; }
        public bool ShowCorrectAnswers { get; set; }
        public bool ShuffleQuestions { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public IEnumerable<QuizQuestionDto> Questions { get; set; } = null!;
    }
}
