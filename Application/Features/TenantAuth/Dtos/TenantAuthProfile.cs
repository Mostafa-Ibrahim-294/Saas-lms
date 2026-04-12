using Application.Features.TenantAuth.Commands.Signup;

namespace Application.Features.TenantAuth.Dtos
{
    public class TenantAuthProfile : Profile
    {
        public TenantAuthProfile()
        {
            CreateMap<SignupCommand, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<ApplicationUser, LoginDto>()
                .ForMember(dest => dest.LastActiveTenant, opt => opt.MapFrom(src => src.LastActiveTenantSubDomain ?? null!));

        }
    }
}
