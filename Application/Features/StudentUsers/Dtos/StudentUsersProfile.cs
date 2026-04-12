namespace Application.Features.StudentUsers.Dtos
{
    public sealed class StudentUsersProfile : Profile
    {
        public StudentUsersProfile()
        {
            CreateMap<ApplicationUser, StudentUserProfileDto>();
        }
    }
}