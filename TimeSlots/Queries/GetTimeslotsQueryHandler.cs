using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TimeSlots.DataBase;
using TimeSlots.Model;

namespace TimeSlots.Queries
{
	public class GetTimeslotsQueryHandler : IRequestHandler<GetTimeslotsQuery, IList<TimeslotDto>>
	{
		private readonly TimeslotsDbContext _context;
		private readonly ILogger<GetTimeslotsQueryHandler> _logger;	
		private readonly TimeOnly GateStart = new(0, 0, 0);
		private readonly TimeOnly GateEnd = new(23, 30, 0);

		public GetTimeslotsQueryHandler(TimeslotsDbContext context, ILogger<GetTimeslotsQueryHandler> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<IList<TimeslotDto>> Handle(GetTimeslotsQuery request, CancellationToken cancellationToken)
		{
			TimeslotsDto dto = new();
			var daysQueue = new Queue<DateTime>(3);
			daysQueue.Enqueue(request.Date.AddDays(-1));
			daysQueue.Enqueue(request.Date);
			daysQueue.Enqueue(request.Date.AddDays(1));

			var minutesNeeded = request.Pallets * Constants.Constants.PalletTime;
			var moscowTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Moscow");

			while(daysQueue.Any())
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

					
					if (timeslot.End >= endTime)
						break;

					var overlaps = false;
					var freeEndTime = default(TimeSpan);
					foreach (var existingTimeslot in _context.Timeslots.Where(t => t.Date.Date == timeslot.Date.Date))
					{
						var existingStart = TimeSpan.Parse(existingTimeslot.From);
						var existingEnd = TimeSpan.Parse(existingTimeslot.To);
						if (timeslot.Start.TimeOfDay < existingEnd && existingStart < timeslot.End.TimeOfDay)
						{
							overlaps = true;
							freeEndTime = existingEnd;
							break;
						}
					}

					if (!overlaps)
					{
						dto.Timeslots.Add(timeslot);
					}
					else
					{
						currentTime = new DateTime(day.Year, day.Month, day.Day, freeEndTime.Hours, freeEndTime.Minutes, freeEndTime.Seconds);
						continue;
					}

					currentTime = currentTime.AddMinutes(roundedMinutes);
				}
			}

			return dto.Timeslots;
		}
	}
}
