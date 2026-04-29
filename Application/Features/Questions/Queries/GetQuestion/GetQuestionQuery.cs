using Application.Features.Questions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Questions.Queries.GetQuestion
{
    public sealed record GetQuestionQuery(int QuestionId) : IRequest<OneOf<SingleQuestionDto, Error>>;
}
