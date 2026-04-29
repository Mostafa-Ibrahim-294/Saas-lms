using Application.Features.TenantStudents.Dtos;

namespace Application.Features.TenantStudents.Queries.GetStudentStatistics
{
    public sealed record GetStudentStatisticsQuery : IRequest<StudentStatisticsDto>;
}