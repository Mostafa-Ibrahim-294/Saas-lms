using Application.Features.ModuleItems.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ModuleItems.Queries.GetQuiz
{
    public sealed record GetQuizQuery(int CourseId, int ModuleId, int ItemId) : IRequest<OneOf<QuizDto, Error>>;
}
