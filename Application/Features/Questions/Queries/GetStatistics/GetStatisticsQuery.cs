using Application.Features.Questions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Questions.Queries.GetStatistics
{
    public sealed record GetStatisticsQuery : IRequest<QuestionStatisticsDto>;
}
