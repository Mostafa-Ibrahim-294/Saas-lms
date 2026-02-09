using Application.Features.Courses.Commands.CreateCourse;
using Application.Features.Courses.Commands.UpdateCourse;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Courses.Dtos
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CreateCourseCommand, Course>()
                .ForMember(dest => dest.PricingType, src => src.MapFrom(c => c.PriceType))
                .ForMember(dest => dest.ThumbnailUrl, src => src.MapFrom(c => c.Thumbnail))
                .ForMember(dest => dest.VideoUrl, src => src.MapFrom(c => c.Video))
                .ForMember(dest => dest.CourseStatus, src => src.MapFrom(c => c.Status));
            CreateMap<UpdateCourseCommand, Course>()
                .ForMember(dest => dest.PricingType, src => src.MapFrom(c => c.PriceType))
                .ForMember(dest => dest.ThumbnailUrl, src => src.MapFrom(c => c.Thumbnail))
                .ForMember(dest => dest.VideoUrl, src => src.MapFrom(c => c.Video))
                .ForMember(dest => dest.CourseStatus, src => src.MapFrom(c => c.Status));
            CreateMap<Course, LookupDto>();
        }
    }
}
