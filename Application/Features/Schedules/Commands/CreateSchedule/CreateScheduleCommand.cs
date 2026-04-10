using Application.Features.Schedules.Dtos;
using Domain.Enums;

namespace Application.Features.Schedules.Commands.CreateSchedule
{
    public sealed record CreateScheduleCommand(string Title, string? Description, DateTime Start, DateTime End, bool AllDay, string Color,
        ScheduleType Type, DateTime? RepeatPeriodEnd, int CourseId, bool RepeatEvent, ScheduleRepeatFrequency? RepeatFrequency)
        : IRequest<OneOf<ScheduleResponse, Error>>;
}