using Application.Features.TenantMembers.Dtos;

namespace Application.Features.TenantStudents.Dtos
{
    public sealed class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.User.ProfilePicture))
                .ForMember(dest => dest.AverageGrades, opt => opt.MapFrom(src => src.StudentGrades.Any() ? (int)src.StudentGrades.Average(sg => sg.Score) : 0))
                .ForMember(dest => dest.EnrolledCourses, opt => opt.MapFrom(src => src.Enrollments.Select(e => e.CourseId).ToList()));

            CreateMap<CourseInvite, ValidateStudentInviteDto>()
               .ForMember(dest => dest.InviterName, opt => opt.MapFrom(src => src.TenantMember.User.FirstName + " " + src.TenantMember.User.LastName))
               .ForMember(dest => dest.IsValid, opt => opt.MapFrom(src => src.AcceptedAt == null && src.ExpiresAt > DateTime.UtcNow))
               .ForMember(dest => dest.IsExpired, opt => opt.MapFrom(src => src.ExpiresAt <= DateTime.UtcNow));
        }
    }
}