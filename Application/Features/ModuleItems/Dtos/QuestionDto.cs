using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ModuleItems.Dtos
{
    public sealed class QuestionDto
    {
        public string? CorrectAnswer { get; set; }
        public Difficulty Difficulty { get; set; }
        public int Category { get; set; }
        public string Question { get; set; } = string.Empty;
        public QuestionType Type { get; set; }
        public List<QuestionOption>? Options { get; set; }
        public int Marks { get; set; }
        public int Order { get; set; }
        public bool RequiresManualGrading { get; set; }
    }

}
