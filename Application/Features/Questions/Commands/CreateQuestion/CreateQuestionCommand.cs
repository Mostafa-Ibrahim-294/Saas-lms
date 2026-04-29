using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Questions.Commands.CreateQuestion
{
    public sealed record CreateQuestionCommand(string? CorrectAnswer, string? Explanation, Difficulty Difficulty, int Category, string Question,
        QuestionType Type, List<QuestionOption>? Options) : IRequest<OneOf<SuccessDto, Error>>;
}
