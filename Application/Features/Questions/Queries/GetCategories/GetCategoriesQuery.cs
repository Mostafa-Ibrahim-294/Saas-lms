using Application.Features.Questions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Questions.Queries.GetCategories
{
    public sealed record GetCategoriesQuery : IRequest<List<QuestionCategoryDto>>;
}
