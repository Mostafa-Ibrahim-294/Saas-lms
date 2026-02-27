using Application.Features.ModuleItems.Commands.CreateModuleItem;
using Application.Features.ModuleItems.Commands.UpdateAssignment;
using Application.Features.ModuleItems.Commands.UpdateLesson;
using Application.Features.ModuleItems.Commands.UpdateSettings;
namespace Application.Features.ModuleItems.Dtos
{
    public class ModuleItemProfile : Profile
    {
        public ModuleItemProfile()
        {
            CreateMap<CreateModuleItemCommand, ModuleItem>();
            CreateMap<ConditionDto, ModuleItemCondition>()
                .ForMember(dest => dest.RequiredModuleItemId, opt => opt.MapFrom(src => src.RequiredItemId))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CreateModuleItemCommand, Lesson>();
            CreateMap<CreateModuleItemCommand, Assignment>()
               .ForMember(dest => dest.Marks, opt => opt.MapFrom(src => src.TotalMarks));
            CreateMap<UpdateLessonCommand, Lesson>()
                .ForPath(dest => dest.ModuleItem.Title, opt => opt.MapFrom(src => src.Title))
                .ForPath(dest => dest.ModuleItem.Description, opt => opt.MapFrom(src => src.Description));
            CreateMap<UpdateAssignmentCommand, Assignment>()
                .ForPath(dest => dest.ModuleItem.Title, opt => opt.MapFrom(src => src.Title))
                .ForPath(dest => dest.ModuleItem.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Marks, opt => opt.MapFrom(src => src.TotalMarks));
            CreateMap<ModuleItem, ItemDto>();
            CreateMap<Assignment, AssignmentDto>()
                .ForMember(dest => dest.TotalMarks, opt => opt.MapFrom(src => src.Marks))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ModuleItemId));
            CreateMap<ModuleItem, SettingsDto>();
            CreateMap<ModuleItemCondition, ConditionDto>()
                .ForMember(dest => dest.RequiredItemId, opt => opt.MapFrom(src => src.RequiredModuleItemId));
            CreateMap<ModuleItem, AllItemsDto>();
            CreateMap<UpdateSettingsCommand, ModuleItem>();


        }
    }
}
