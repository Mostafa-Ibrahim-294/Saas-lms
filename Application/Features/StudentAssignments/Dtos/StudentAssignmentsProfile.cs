namespace Application.Features.StudentAssignments.Dtos
{
    public sealed class StudentAssignmentsProfile : Profile
    {
        public StudentAssignmentsProfile()
        {
            CreateMap<ModuleItem, StudentAssignmentDto>()
                .ForMember(dest => dest.Assignment, opt => opt.MapFrom(src => src.Assignment))
                .ForMember(dest => dest.AssignmentSubmission, opt => opt.MapFrom(src => src.Assignment!.Submissions.FirstOrDefault()));

            CreateMap<Assignment, AssignmentDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.ModuleItem.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ModuleItem.Description))
                .ForMember(dest => dest.TotalMarks, opt => opt.MapFrom(src => src.Marks));

            CreateMap<AssignmentSubmission, AssignmentSubmissionDto>()
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.EarnedMarks))
                .ForMember(dest => dest.SubmissionFiles, opt => opt.MapFrom(src => src.File))
                .ForMember(dest => dest.GradedBy, opt => opt.MapFrom(src => src.Student.StudentGrades.Select(sg => sg.GraderId).FirstOrDefault()))
                .ForMember(dest => dest.GradedAt, opt => opt.MapFrom(src => src.Student.StudentGrades.Select(sg => sg.GradedAt).FirstOrDefault()));

            CreateMap<Domain.Entites.File, SubmissionFileDto>()
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.Name));
        }
    }
}
