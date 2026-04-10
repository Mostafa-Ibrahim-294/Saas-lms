using Application.Features.Schedules.Dtos;
using Domain.Enums;

namespace Application.Features.Schedules.Commands.UpdateSchedule
{
    public sealed record UpdateScheduleCommand(int ScheduleId, string Title, string? Description, DateTime Start, DateTime End, bool AllDay,
        string Color, ScheduleType Type, DateTime? RepeatPeriodEnd, int CourseId, bool RepeatedEvent, ScheduleRepeatFrequency? RepeatFrequency)
        : IRequest<OneOf<ScheduleResponse, Error>>;
}