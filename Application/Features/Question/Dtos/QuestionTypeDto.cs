using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Questions.Dtos
{
    public sealed class QuestionTypeDto
    {
        public QuestionType Type { get; set; }
        public int Count { get; set; }
    }
}
