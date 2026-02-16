namespace Application.Features.Tenants.Dtos
{
    public sealed class TenantPermissionProfile : Profile
    {
        public TenantPermissionProfile()
        {
            CreateMap<Permission, TenantPermissionDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description ?? string.Empty));
        }
    }
}