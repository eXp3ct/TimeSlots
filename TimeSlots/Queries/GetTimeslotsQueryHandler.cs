using MediatR;
using Microsoft.EntityFrameworkCore;
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
			var timeslots = new List<TimeslotDto>();
			var nearbyDates = await EnqueueNearbyDates(request.Date);

			var minutesNeeded = GetNeededMinutes(request.Pallets);
			var isWrapped = false;
			var lastEndTime = DateTime.MinValue;
			var schedules = await _context.GateSchedules.ToListAsync(cancellationToken);
			foreach(var day in  nearbyDates)
			{
				if (!schedules.Any(s => s.DaysOfWeek.Contains(day.Date.DayOfWeek) && s.TaskTypes.Contains(request.TaskType)))
					continue;
				var currentTime = isWrapped ? lastEndTime : day.FromTime(_gateStart);
				var endTime = day.FromTime(_gateEnd);
				
				while(currentTime <= endTime)
				{
					var timeslot = new TimeslotDto(day)
					{
						Start = currentTime,
						End = currentTime.AddMinutes(minutesNeeded),
						TaskType = request.TaskType
					};

					if(timeslot.End >= endTime)
					{
						isWrapped = true;
						lastEndTime = timeslot.End;
					}
					else
					{
						isWrapped = false;
					}

					var overlapingTimeslot = await CheckForOverlaps(timeslot);
					if (overlapingTimeslot == null)
					{
						timeslots.Add(timeslot);
					}
					else
					{
						currentTime = overlapingTimeslot.To;
						continue;
					}

					currentTime = timeslot.End;
				}
			}

			return timeslots;
		}


		private async Task<Timeslot?> CheckForOverlaps(TimeslotDto timeslot)
		{
			
			var gateSchedules = await _context.GateSchedules.ToListAsync();
			var gateIds = gateSchedules
				.Where(s => s.TaskTypes.Contains(timeslot.TaskType) && s.DaysOfWeek.Contains(timeslot.Date.DayOfWeek))
				.Select(g => g.GateId).ToList();

			var gates = await _context.Gates
				.Include(x => x.Timeslots)
				.Include(x => x.GateSchedules)
				.Where(g => gateIds.Contains(g.Id))
				.ToListAsync();

			var existingTimeslots = await _context.Timeslots
				.Where(t => t.Date.Date == timeslot.Date.Date || t.Date <= timeslot.Date)
				.ToListAsync();
			var hasOverlap = false;
			Timeslot? dto = new();

			foreach(var gate in gates)
			{
				var gateOverlap = false;
				foreach(var existingTimeslot in gate.Timeslots)
				{
					if ((timeslot.Start < existingTimeslot.To && existingTimeslot.From < timeslot.End) || // Пересечение временных отрезков
					(timeslot.Start == existingTimeslot.To && existingTimeslot.From == timeslot.End) || // Одинаковые временные отрезки
					(timeslot.Start <= existingTimeslot.From && timeslot.End >= existingTimeslot.To) || // Один временной отрезок полностью включает другой
					(timeslot.Start >= existingTimeslot.From && timeslot.End <= existingTimeslot.To)) // Один временной отрезок полностью включен в другой
					{
						gateOverlap = true;
						dto = existingTimeslot;
						break;
					}
				}
				if (!gateOverlap)
					return null;
				hasOverlap = gateOverlap;
			}

			return hasOverlap ? dto : null;
		}

		private async Task<IEnumerable<DateTime>> EnqueueNearbyDates(DateTime date)
		{
			return await Task.FromResult(new List<DateTime>()
			{
				date.AddDays(-1),
				date,
				date.AddDays(1)
			});
		}

		private int GetNeededMinutes(int pallets)
		{
			var minutesNeeded = pallets * PalletTime;
			return (int)Math.Ceiling(minutesNeeded / (float)TimeParity) * TimeParity;
		}
	}
}
