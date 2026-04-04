using Application.Features.TenantStudents.Dtos;

namespace Application.Features.TenantStudents.Queries.GetStudentsByCourseId
{
    public sealed record GetStudentsByCourseIdQuery(int CourseId) : IRequest<OneOf<List<StudentDto>, Error>>;
}