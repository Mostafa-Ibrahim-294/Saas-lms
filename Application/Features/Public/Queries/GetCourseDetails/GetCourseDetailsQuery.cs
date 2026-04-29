using Application.Features.Public.Dtos;

namespace Application.Features.Public.Queries.GetCourseDetails
{
    public sealed record GetCourseDetailsQuery(int CourseId) : IRequest<OneOf<WebsiteCourseDetailsDto, Error>>;
}