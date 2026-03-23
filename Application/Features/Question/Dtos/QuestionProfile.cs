using Application.Features.Questions.Commands.CreateQuestion;
using Application.Features.Questions.Commands.UpdateQuestion;

namespace Application.Features.Questions.Dtos
{
    public sealed class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<CreateQuestionCommand, Question>()
                .ForMember(dest => dest.QuestionCategoryId, src => src.MapFrom(src => src.Category))
                .ForMember(dest => dest.QuestionTitle, src => src.MapFrom(src => src.Question));
            CreateMap<UpdateQuestionCommand, Question>()
               .ForMember(dest => dest.QuestionCategoryId, src => src.MapFrom(src => src.Category))
               .ForMember(dest => dest.QuestionTitle, src => src.MapFrom(src => src.Question));
            CreateMap<Question, QuestionResponse>()
                    .ForMember(dest => dest.Question, src => src.MapFrom(src => src.QuestionTitle));
            CreateMap<QuestionCategory, QuestionCategoryDto>()
                .ForMember(dest => dest.TotalQuestions, src => src.MapFrom(src => src.Questions.Count));
            CreateMap<Question, SingleQuestionDto>()
                .ForMember(dest => dest.Question, src => src.MapFrom(src => src.QuestionTitle))
                .ForMember(dest => dest.Category, src => src.MapFrom(src => src.QuestionCategory));
            CreateMap<QuestionCategory, CategoryDto>();
                CreateMap<Question, AllQuestionsDto>()
                    .ForMember(dest => dest.Question, src => src.MapFrom(src => src.QuestionTitle))
                    .ForMember(dest => dest.Category, src => src.MapFrom(src => src.QuestionCategory));



        }

    }
}
