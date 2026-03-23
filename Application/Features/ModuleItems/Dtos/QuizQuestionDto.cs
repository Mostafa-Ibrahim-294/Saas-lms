using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ModuleItems.Dtos
{
    public sealed class QuizQuestionDto
    {
        public int Id { get; set; }
        public string? CorrectAnswer { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public QuestionType Type { get; set; }
        public List<QuestionOption>? Options { get; set; }
        public int Marks { get; set; }
        public int Order { get; set; }
    }
}
