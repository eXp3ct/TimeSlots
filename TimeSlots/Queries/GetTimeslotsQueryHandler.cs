using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TimeSlots.DataBase;
using TimeSlots.Extensions;
using TimeSlots.Model;

namespace TimeSlots.Queries
{
	public class GetTimeslotsQueryHandler : IRequestHandler<GetTimeslotsQuery, IEnumerable<TimeslotDto>>
	{
		private const int PalletTime = 5;
		private const int TimeParity = 30;
		private readonly TimeslotsDbContext _context;
		private readonly TimeOnly _gateStart = new(0, 0, 0);
		private readonly TimeOnly _gateEnd = new(23, 30, 0);

		public GetTimeslotsQueryHandler(TimeslotsDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<TimeslotDto>> Handle(GetTimeslotsQuery request, CancellationToken cancellationToken)
		{
			var dto = new List<TimeslotDto>();
			var daysQueue = new Queue<DateTime>(3);
			daysQueue.Enqueue(request.Date.AddDays(-1));
			daysQueue.Enqueue(request.Date);
			daysQueue.Enqueue(request.Date.AddDays(1));

			var minutesNeeded = request.Pallets * PalletTime;
			var wrapped = default(bool);
			var wrappedTime = default(DateTime);
			while (daysQueue.Any())
			{
				var day = daysQueue.Dequeue();
				DateTime currentTime;
				if (wrapped)
				{
					currentTime = wrappedTime;
				}
				else
				{
					currentTime = day.FromTime(_gateStart); 
				}
				var endTime = day.FromTime(_gateEnd);

				while (currentTime <= endTime)
				{
					var roundedMinutes = (int)Math.Ceiling(minutesNeeded / (float)TimeParity) * TimeParity;
					var timeslot = new TimeslotDto(day)
					{
						Start = currentTime,
						End = currentTime.AddMinutes(roundedMinutes)
					};					

					if(timeslot.End >= endTime)
					{
						wrapped = true;
						wrappedTime = timeslot.End;
					}

					var overlaps = await CheckForOverlaps(timeslot); // Асинхронная проверка на перекрытия

					if (!overlaps)
					{
						dto.Add(timeslot);
					}
					else
					{
						currentTime = new DateTime(day.Year, day.Month, day.Day, timeslot.End.Hour, timeslot.End.Minute, timeslot.End.Second);
						continue;
					}

					currentTime = currentTime.AddMinutes(roundedMinutes);
				}
			}

			return dto;
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
