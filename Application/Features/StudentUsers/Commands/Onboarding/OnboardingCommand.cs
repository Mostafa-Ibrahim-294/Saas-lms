using Application.Features.TenantStudents.Dtos;

namespace Application.Features.StudentUsers.Commands.Onboarding
{
    public sealed record OnboardingCommand(string Grade, string Semester, string Goal, List<string> Subjects,
        Dictionary<string, int> Confidence) : IRequest<OneOf<StudentResponse, Error>>;
}