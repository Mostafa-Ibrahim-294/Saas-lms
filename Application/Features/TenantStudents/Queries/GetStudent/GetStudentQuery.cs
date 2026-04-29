using Application.Features.TenantOrders.Dtos;
namespace Application.Features.TenantStudents.Queries.GetStudent
{
    public sealed record GetStudentQuery(int StudentId) : IRequest<OneOf<Dtos.StudentDto, Error>>;
}
