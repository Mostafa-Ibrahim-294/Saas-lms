using Application.Features.StudentUsers.Commands.Onboarding;

namespace Application.Features.StudentUsers.Dtos
{
    public sealed class StudentUsersProfile : Profile
    {
        public StudentUsersProfile()
        {
            CreateMap<ApplicationUser, StudentUserProfileDto>();

            CreateMap<OnboardingCommand, Student>();
        }
    }
}