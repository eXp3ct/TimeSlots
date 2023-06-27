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
		private readonly TimeOnly GateStart = new(0, 0, 0);
		private readonly TimeOnly GateEnd = new(23, 30, 0);

		public GetTimeslotsQueryHandler(TimeslotsDbContext context)
		{
			_context = context;
		}

		public async Task<IList<TimeslotDto>> Handle(GetTimeslotsQuery request, CancellationToken cancellationToken)
		{
			TimeslotsDto dto = new();
			var daysQueue = new Queue<DateTime>(3);
			daysQueue.Enqueue(request.Date);
			daysQueue.Enqueue(request.Date.AddDays(1));
			daysQueue.Enqueue(request.Date.AddDays(-1));

			var minutesNeeded = request.Pallets * Constants.Constants.PalletTime;
			var moscowTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Moscow");

			while(daysQueue.Count > 0)
			{
				var day = daysQueue.Dequeue();

				var currentTime = TimeZoneInfo.ConvertTime(new DateTime(day.Year, day.Month,
				day.Day, GateStart.Hour, GateStart.Minute, GateStart.Second), moscowTimeZone);
				var endTime = TimeZoneInfo.ConvertTime(new DateTime(day.Year, day.Month,
					day.Day, GateEnd.Hour, GateEnd.Minute, GateEnd.Second), moscowTimeZone);

				while (currentTime <= endTime)
				{
					var roundedMinutes = (int)Math.Ceiling(minutesNeeded / 30f) * 30;
					var timeslot = new TimeslotDto(request.Date)
					{
						Start = currentTime.TimeOfDay,
						End = currentTime.AddMinutes(roundedMinutes).TimeOfDay
					};



					// Check if the new time slot overlaps with any existing time slot
					bool overlaps = false;
					foreach (var existingTimeslot in _context.Timeslots)
					{
						var existingStart = TimeSpan.Parse(existingTimeslot.From);
						var existingEnd = TimeSpan.Parse(existingTimeslot.To);
						if (timeslot.Start < existingEnd && existingStart < timeslot.End)
						{
							overlaps = true;
							break;
						}
					}

					if (!overlaps)
					{
						dto.Timeslots.Add(timeslot);
					}


					currentTime = currentTime.AddMinutes(roundedMinutes);
				}
			}

			return dto.Timeslots;
		}
	}
}
