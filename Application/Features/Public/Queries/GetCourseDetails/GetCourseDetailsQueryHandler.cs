using Application.Contracts.Repositories;
using Application.Features.Public.Dtos;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Public.Queries.GetCourseDetails
{
    internal sealed class GetCourseDetailsQueryHandler : IRequestHandler<GetCourseDetailsQuery, OneOf<WebsiteCourseDetailsDto, Error>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetCourseDetailsQueryHandler(ICourseRepository courseRepository, IHttpContextAccessor httpContextAccessor)
        {
            _courseRepository = courseRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<OneOf<WebsiteCourseDetailsDto, Error>> Handle(GetCourseDetailsQuery request, CancellationToken cancellationToken)
        {
            var studentId = _httpContextAccessor.HttpContext?.Session.GetString(AuthConstants.StudentId);
            var websiteCourseDetails = await _courseRepository.GetWebsiteCourseDetailsAsync(request.CourseId, request.SubDomain, studentId, cancellationToken);
            if (websiteCourseDetails is null)
                return CourseErrors.CourseNotFound;

            return websiteCourseDetails;
        }
    }
}
