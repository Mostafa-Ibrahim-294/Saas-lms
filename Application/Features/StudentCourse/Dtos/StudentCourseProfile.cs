using Domain.Enums;

namespace Application.Features.StudentCourse.Dtos
{
    public sealed class StudentCourseProfile : Profile
    {
        public StudentCourseProfile()
        {
            CreateMap<Enrollment, StudentCoursesDto>()
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
                        .FirstOrDefault())
                );


            CreateMap<Enrollment, StudentCourseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CourseId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Course.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Course.Description))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Course.Subject.Label))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Course.Grade.Label))
                .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => new TeacherDto
                {
                    Name = $"{src.Course.CreatedBy.FirstName} {src.Course.CreatedBy.LastName}",
                    Email = src.Course.CreatedBy.Email!,
                    ProfilePicture = src.Course.CreatedBy.ProfilePicture
                }))
                .ForMember(dest => dest.TotalModules, opt => opt.MapFrom(src => src.Course.Modules.Count))
                //.ForMember(dest => dest.CompletedModules, opt => opt.MapFrom(src => src.Course.CourseProgresses
                //        .Where(cp => cp.StudentId == src.StudentId)
                //        .Select(cp => cp.CompletedModules)
                //        .FirstOrDefault()))
                .ForMember(dest => dest.SubscriptionStatus, opt => opt.MapFrom(src => src.Course.StudentSubscriptions
                        .Where(ss => ss.StudentId == src.StudentId && ss.CourseId == src.CourseId)
                        .Select(ss => ss.Status)
                        .FirstOrDefault()))
                .ForMember(dest => dest.RenewDate, opt => opt.MapFrom(src => src.Course.StudentSubscriptions
                        .Where(ss => ss.StudentId == src.StudentId && ss.CourseId == src.CourseId)
                        .Select(ss => ss.EndDate)
                        .FirstOrDefault()))
                .ForMember(dest => dest.CurrentModuleItem, opt => opt.MapFrom(src => src.ModuleItem));

            CreateMap<ModuleItem, CurrentModuleItemDto>()
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.Assignment != null ? src.Assignment.DueDate : (DateTime?)null));
        }
    }
}