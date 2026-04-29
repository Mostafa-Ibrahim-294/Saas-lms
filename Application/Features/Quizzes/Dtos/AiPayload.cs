using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Quizzes.Dtos
{
    public sealed record AiPayload(string? Prompt, QuizMetadata Metadata, Difficulty Difficulty, QuestionType Type,
        int QuestionsNumber);
}
