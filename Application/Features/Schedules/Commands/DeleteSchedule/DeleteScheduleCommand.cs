using Application.Features.Schedules.Dtos;

namespace Application.Features.Schedules.Commands.DeleteSchedule
{
    public sealed record DeleteScheduleCommand(int ScheduleId) : IRequest<OneOf<ScheduleResponse, Error>>;
}