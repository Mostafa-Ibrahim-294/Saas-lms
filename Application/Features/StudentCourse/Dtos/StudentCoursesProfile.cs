namespace Application.Features.StudentCourse.Dtos
{
    public sealed class StudentCoursesProfile : Profile
    {
        public StudentCoursesProfile()
        {
            CreateMap<Enrollment, StudentCourseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CourseId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Course.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Course.Description))
                .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => src.Course.ThumbnailUrl))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Course.Tags))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Course.Subject.Label))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Course.Grade.Label))
                .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => new TeacherDto
                {
                    Name = $"{src.Course.CreatedBy.FirstName} {src.Course.CreatedBy.LastName}",
                    Email = src.Course.CreatedBy.Email!,
                    ProfilePicture = src.Course.CreatedBy.ProfilePicture
                }))
                .ForMember(dest => dest.TotalStudents, opt => opt.MapFrom(src => src.Course.Enrollments.Count))
                .ForMember(dest => dest.TotalLessons, opt => opt.MapFrom(src => src.Course.CourseProgresses
                        .Where(cp => cp.StudentId == src.StudentId)
                        .Select(cp => cp.TotalLessons)
                        .FirstOrDefault()))
                .ForMember(dest => dest.CompletedLessons, opt => opt.MapFrom(src => src.Course.CourseProgresses
                        .Where(cp => cp.StudentId == src.StudentId)
                        .Select(cp => cp.CompletedLessons)
                        .FirstOrDefault()))
                .ForMember(dest => dest.Progress, opt => opt.MapFrom(src => src.Course.CourseProgresses
                        .Where(cp => cp.StudentId == src.StudentId)
                        .Select(cp => cp.TotalLessons == 0 ? 0 : ((double)cp.CompletedLessons * 100 / cp.TotalLessons))
                        .FirstOrDefault()))
                .ForMember(dest => dest.SubscriptionStatus, opt => opt.MapFrom(src => src.Course.StudentSubscriptions
                        .Where(ss => ss.StudentId == src.StudentId)
                        .Select(ss => ss.Status)
                        .FirstOrDefault()))
                .ForMember(dest => dest.RenewDate, opt => opt.MapFrom(src => src.Course.StudentSubscriptions
                        .Where(ss => ss.StudentId == src.StudentId)
                        .Select(ss => ss.EndDate)
                        .FirstOrDefault()));
        }
    }
}