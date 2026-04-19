using Domain.Enums;

namespace Application.Features.StudentLessons.Dtos
{
    public sealed class StudentLessonsProfile : Profile
    {
        public StudentLessonsProfile()
        {
            CreateMap<ModuleItem, StudentLessonItemDto>()
                .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.Lesson != null && src.Lesson.LessonViews.Any(lv => lv.Status == ViewStatus.Completed)))
                .ForMember(dest => dest.Resources, opt => opt.MapFrom(src => src.Lesson!.Resources))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Lesson!.File));

            CreateMap<Domain.Entites.File, ContentDto>()
                .ForMember(dest => dest.VideoId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.VideoUrl, opt => opt.MapFrom(src => src.Url));
        }
    }
}