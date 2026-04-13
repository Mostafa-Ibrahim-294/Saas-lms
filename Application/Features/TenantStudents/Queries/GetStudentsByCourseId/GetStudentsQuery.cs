using Application.Features.TenantStudents.Dtos;

namespace Application.Features.TenantStudents.Queries.GetStudentsByCourseId
{
    public sealed record GetStudentsQuery(int? CourseId) : IRequest<OneOf<List<StudentsDto>, Error>>;
}