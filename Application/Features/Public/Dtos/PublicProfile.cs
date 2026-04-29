namespace Application.Features.Public.Dtos
{
    public sealed class PublicProfile : Profile
    {
        public PublicProfile()
        {
            CreateMap<Course, WebsiteCourseDetailsDto>()
                .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(src => src.ThumbnailUrl))
                .ForMember(dest => dest.Video, opt => opt.MapFrom(src => src.VideoUrl))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject.Label))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade.Label))
                .ForMember(dest => dest.StudentsCount, opt => opt.MapFrom(src => src.Enrollments.Count))
                .ForMember(dest => dest.ModulesCount, opt => opt.MapFrom(src => src.Modules.Count))
                .ForMember(dest => dest.LessonsCount, opt => opt.MapFrom(src => src.Lessons.Count))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags != null ? src.Tags.ToList() : new()))
                .ForMember(dest => dest.BillingCycle, opt => opt.MapFrom(src => src.BillingCycle.HasValue ? src.BillingCycle : null))
                .ForMember(dest => dest.Instructor, opt => opt.MapFrom(src => src.Tenant.TenantMembers.FirstOrDefault(m => m.UserId == src.CreatedById)))
                .ForMember(dest => dest.Modules, opt => opt.MapFrom(src => src.Modules.OrderBy(m => m.Order)))
                .ForMember(dest => dest.IsEnrolled, opt => opt.Ignore())
                .ForMember(dest => dest.HasPendingOrder, opt => opt.Ignore());

            CreateMap<TenantMember, CourseInstructorDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.User.ProfilePicture));

            CreateMap<Module, CourseModuleDto>()
                .ForMember(dest => dest.LessonsCount, opt => opt.MapFrom(src => src.Course.Lessons.Count(l => l.ModuleId == src.Id)));

            CreateMap<PaymentMethod, PublicPaymentMethodDto>();

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentType))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Course.Currency))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course));

            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title));
        }
    }
}