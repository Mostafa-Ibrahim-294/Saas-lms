using Application.Contracts.Repositories;
using Application.Features.TenantStudents.Dtos;
using Microsoft.AspNetCore.Http;

namespace Application.Features.TenantStudents.Queries.GetStudentsByCourseId
{
    public sealed class GetStudentByCourseIdQueryHandler : IRequestHandler<GetStudentsByCourseIdQuery, OneOf<List<StudentDto>, Error>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetStudentByCourseIdQueryHandler(IStudentRepository studentRepository, ICourseRepository courseRepository, IHttpContextAccessor httpContextAccessor)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<OneOf<List<StudentDto>, Error>> Handle(GetStudentsByCourseIdQuery request, CancellationToken cancellationToken)
        {
            var subDomain = _httpContextAccessor?.HttpContext?.Request.Cookies[AuthConstants.SubDomain];
            var courseExists = await _courseRepository.GetCourseByIdAsync(request.CourseId, subDomain!, cancellationToken);
            if (courseExists is null)
                return CourseErrors.CourseNotFound;

            return await _studentRepository.GetStudentsByCourseIdAsync(request.CourseId, cancellationToken);
        }
    }
}