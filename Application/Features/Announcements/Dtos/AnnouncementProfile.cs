namespace Application.Features.Announcements.Dtos
{
    public sealed class AnnouncementProfile : Profile
    {
        public AnnouncementProfile()
        {
            CreateMap<Announcement, AnnouncementDto>()
                .ForMember(dest => dest.TargetCourses, opt => opt.MapFrom(src => src.TargetCourseIds.Select(tc => tc).ToArray()));
        }
    }
}