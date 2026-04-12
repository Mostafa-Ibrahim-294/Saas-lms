using Application.Features.StudentAuth.Commands.Signup;

namespace Application.Features.StudentAuth.Dtos
{
    public sealed class StudentAuthProfile : Profile
    {
        public StudentAuthProfile()
        {
            CreateMap<SignUpCommand, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}