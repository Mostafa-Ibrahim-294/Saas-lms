using Application.Contracts.Repositories;
using Application.Features.Schedules.Dtos;
using Microsoft.AspNetCore.Http;
using TimeZoneConverter;

namespace Application.Features.Schedules.Commands.UpdateSchedule
{
    internal sealed class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, OneOf<ScheduleResponse, Error>>
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UpdateScheduleCommandHandler(IScheduleRepository scheduleRepository, IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<OneOf<ScheduleResponse, Error>> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
        {
            var subDomain = _httpContextAccessor.HttpContext?.Request.Cookies[AuthConstants.SubDomain];

            var existingSchedule = await _scheduleRepository.GetScheduleByIdAsync(request.ScheduleId, subDomain!, cancellationToken);
            if (existingSchedule is null)
                return ScheduleErrors.ScheduleNotFound;

            var startAt = request.Start;
            var endAt = request.End;
            if (request.AllDay)
            {
                var egyptTimeZone = TZConvert.GetTimeZoneInfo("Egypt Standard Time");
                var startEgypt = TimeZoneInfo.ConvertTimeFromUtc(startAt, egyptTimeZone);
                var date = startEgypt.Date;
                var startOfDayEgypt = date;
                var endOfDayEgypt = date.AddDays(1);

                startAt = TimeZoneInfo.ConvertTimeToUtc(startOfDayEgypt, egyptTimeZone);
                endAt = TimeZoneInfo.ConvertTimeToUtc(endOfDayEgypt, egyptTimeZone);
            }

            var hasConflict = await _scheduleRepository.HasConflictAsync(subDomain!, startAt, endAt, request.AllDay, cancellationToken, request.ScheduleId);
            if (hasConflict)
                return ScheduleErrors.ScheduleConflict;

            _mapper.Map(request, existingSchedule);
            existingSchedule.StartAt = startAt;
            existingSchedule.EndAt = endAt;

            await _scheduleRepository.UpdateScheduleAsync(existingSchedule, cancellationToken);
            await _scheduleRepository.SaveAsync(cancellationToken);
            return new ScheduleResponse { Message = MessagesConstants.ScheduleUpdated };
        }
    }
}