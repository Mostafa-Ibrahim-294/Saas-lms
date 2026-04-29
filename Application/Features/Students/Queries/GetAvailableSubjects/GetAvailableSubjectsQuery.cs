using Application.Features.Students.Dtos;

namespace Application.Features.Students.Queries.GetAvailableSubjects
{
    public sealed record GetAvailableSubjectsQuery : IRequest<List<AvailableSubjectDto>>;
}