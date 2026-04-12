using Application.Features.StudentAuth.Commands.Signup;

namespace Application.Features.StudentAuth.Dtos
{
    public sealed class StudentAuthProfile : Profile
    {
        public StudentAuthProfile()
        {
            CreateMap<SignUpCommand, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<SignUpCommand, Student>()
                .ForMember(dest => dest.ParentEmail, opt => opt.MapFrom(src => src.ParentEmail));
        }
    }
}