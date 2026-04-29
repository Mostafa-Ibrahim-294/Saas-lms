using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Quizzes.Commands.CreateAiQuizQuestions
{
    public sealed record CreateAiQuizQuestionsCommand(int CourseId, int ModuleId, int ItemId, string? Prompt
        , Difficulty Difficulty, QuestionType Type, int QuestionsNumber) : IRequest<OneOf<bool, Error>>;
}
