using Application.Features.Tenants.Commands.CreateOnboarding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Tenants.Dtos
{
    public class TenantProfile : Profile
    {
        public TenantProfile()
        {
            CreateMap<LabelValueDto, Subject>();
            CreateMap<LabelValueDto, TeachingLevel>();
            CreateMap<LabelValueDto, Grade>();
            CreateMap<CreateOnboardingCommand, Tenant>();
            CreateMap<CreateOnboardingCommand, ApplicationUser>()
                .ForMember(dest => dest.LastActiveTenantSubDomain, opt => opt.MapFrom(src => src.SubDomain));
            CreateMap<CreateOnboardingCommand, TenantMember>();


        }
    }
}
