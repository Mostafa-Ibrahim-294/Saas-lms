using Application.Features.Questions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Questions.Queries.GetAllQuestions
{
    public sealed record GetAllQuestionsQuery : IRequest<IEnumerable<AllQuestionsDto>>;
}
