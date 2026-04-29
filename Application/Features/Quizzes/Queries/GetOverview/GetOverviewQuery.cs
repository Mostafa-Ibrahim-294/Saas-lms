using Application.Features.Quizzes.Dtos;

namespace Application.Features.Quizzes.Queries.GetOverview
{
    public sealed record GetOverviewQuery(int CourseId, int ModuleId, int ItemId) : IRequest<OneOf<OverviewDto, Error>>;
}
