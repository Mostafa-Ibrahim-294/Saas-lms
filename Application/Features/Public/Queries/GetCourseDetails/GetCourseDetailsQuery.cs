using Application.Features.Public.Dtos;

namespace Application.Features.Public.Queries.GetCourseDetails
{
    public sealed record GetCourseDetailsQuery(int CourseId, string SubDomain) : IRequest<OneOf<WebsiteCourseDetailsDto, Error>>;
}