using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TimeSlots.DataBase;
using TimeSlots.Model;

namespace TimeSlots.Queries
{
    public class GetTimeslotsQueryHandler : IRequestHandler<GetTimeslotsQuery, IEnumerable<TimeslotDto>>
    {
        private readonly TimeslotsDbContext _context;
        private readonly TimeOnly GateStart = new(0, 0, 0);
        private readonly TimeOnly GateEnd = new(23, 30, 0);

        public GetTimeslotsQueryHandler(TimeslotsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TimeslotDto>> Handle(GetTimeslotsQuery request, CancellationToken cancellationToken)
        {
            var dto = new TimeslotsDto();
            var daysQueue = new Queue<DateTime>(3);
            daysQueue.Enqueue(request.Date.AddDays(-1));
            daysQueue.Enqueue(request.Date);
            daysQueue.Enqueue(request.Date.AddDays(1));

            var minutesNeeded = request.Pallets * Constants.Constants.PalletTime;

            //Убрать
            var moscowTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Moscow");

            while (daysQueue.Any())
            {
                var day = daysQueue.Dequeue();

                var currentTime = TimeZoneInfo.ConvertTime(new DateTime(day.Year, day.Month,
                    day.Day, GateStart.Hour, GateStart.Minute, GateStart.Second), moscowTimeZone);
                var endTime = TimeZoneInfo.ConvertTime(new DateTime(day.Year, day.Month,
                    day.Day, GateEnd.Hour, GateEnd.Minute, GateEnd.Second), moscowTimeZone);

                while (currentTime <= endTime)
                {
                    var roundedMinutes = (int)Math.Ceiling(minutesNeeded / 30f) * 30;
                    var timeslot = new TimeslotDto(day)
                    {
                        Start = currentTime,
                        End = currentTime.AddMinutes(roundedMinutes)
                    };

                    //ToDo перенос на след день
                    if (timeslot.End >= endTime)
                        break;

                    var overlaps = await CheckForOverlaps(timeslot); // Асинхронная проверка на перекрытия

                    if (!overlaps)
                    {
                        dto.Timeslots.Add(timeslot);
                    }
                    else
                    {
                        currentTime = new DateTime(day.Year, day.Month, day.Day, timeslot.End.Hour, timeslot.End.Minute, timeslot.End.Second);
                        continue;
                    }

                    currentTime = currentTime.AddMinutes(roundedMinutes);
                }
            }

            return dto.Timeslots;
        }

        private async Task<bool> CheckForOverlaps(TimeslotDto timeslot)
        {
            var existingTimeslots = await _context.Timeslots
                .Where(t => t.Date.Date == timeslot.Date.Date)
                .ToListAsync();

            foreach (var existingTimeslot in existingTimeslots)
            {
                var existingStart = TimeSpan.Parse(existingTimeslot.From);
                var existingEnd = TimeSpan.Parse(existingTimeslot.To);
                if (timeslot.Start.TimeOfDay < existingEnd && existingStart < timeslot.End.TimeOfDay)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
