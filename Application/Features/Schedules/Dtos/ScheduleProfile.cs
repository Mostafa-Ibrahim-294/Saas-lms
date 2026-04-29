using Application.Features.Schedules.Commands.CreateSchedule;
using Application.Features.Schedules.Commands.UpdateSchedule;

namespace Application.Features.Schedules.Dtos
{
    public sealed class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<CreateScheduleCommand, Schedule>()
                .ForMember(dest => dest.RepeatEvent, opt => opt.MapFrom(src => src.RepeatedEvent));

            CreateMap<UpdateScheduleCommand, Schedule>();

            CreateMap<Schedule, ScheduleDto>()
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.StartAt))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.EndAt));
        }
    }
}