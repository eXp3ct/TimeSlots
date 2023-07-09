using Humanizer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
		private TimeOnly _gateStart = new(0, 0, 0);
		private TimeOnly _gateEnd = new(23, 30, 0);

		public GetTimeslotsQueryHandler(TimeslotsDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<TimeslotDto>> Handle(GetTimeslotsQuery request, CancellationToken cancellationToken)
		{
			Company? company = await _context.Companies.Where(c => c.Id == request.CompanyId).FirstOrDefaultAsync(cancellationToken); // берем компанию
			List<PlatformFavorite>? platformFavorites = null;
			if(company != null) // если найдена компания, то берем к ней расписания
			{
				platformFavorites = await _context.PlatformFavorites
					.Where(p => p.CompanyId == company.Id).ToListAsync(cancellationToken);
			}
			var timeslots = new List<TimeslotDto>();
			var nearbyDates = await EnqueueNearbyDates(request.Date);

			var minutesNeeded = GetNeededMinutes(request.Pallets);
			var isWrapped = false;
			var lastEndTime = DateTime.MinValue;
			var schedules = await _context.GateSchedules.ToListAsync(cancellationToken); // все расписания
			foreach(var day in  nearbyDates)
			{
				var scheduleExist = false;
				var platformFavoriteExist = false;

				bool predicateGateSchedule(GateSchedule s) => s.DaysOfWeek.Contains(day.Date.DayOfWeek) && s.TaskTypes.Contains(request.TaskType);
				bool predicatePlatformFavorite(PlatformFavorite s) => s.DaysOfWeek.Contains(day.Date.DayOfWeek) && s.TaskTypes.Contains(request.TaskType);

				if (platformFavorites == null || !platformFavorites.Any(predicatePlatformFavorite))
				{
					if (schedules.Any(predicateGateSchedule))
					{
						scheduleExist = true;
						var schedule = schedules.Where(predicateGateSchedule).FirstOrDefault();
						_gateStart = schedule.From.ToTimeOnly();
						_gateEnd = schedule.To.ToTimeOnly();
					}
					else
					{
						scheduleExist = false;
						_gateStart = new(0, 0, 0);
						_gateEnd = new(23, 30, 0);
					}
				}
				else
				{
					platformFavoriteExist = true;
					var platformFavorite = platformFavorites.Where(predicatePlatformFavorite).FirstOrDefault();

					if (!await CheckForAvaliableTaskCount(request))
					{
						platformFavoriteExist = false;

						if (schedules.Any(predicateGateSchedule))
						{
							scheduleExist = true;
							var schedule = schedules.Where(predicateGateSchedule).FirstOrDefault();
							_gateStart = schedule.From.ToTimeOnly();
							_gateEnd = schedule.To.ToTimeOnly();
						}
						else
						{
							scheduleExist = false;
							_gateStart = new(0, 0, 0);
							_gateEnd = new(23, 30, 0);
						}
					}
					else
					{
						_gateStart = platformFavorite.From.ToTimeOnly();
						_gateEnd = platformFavorite.To.ToTimeOnly();
					}
				}
				var currentTime = DateTime.MinValue;
				if(isWrapped)
				{
                    if (scheduleExist || platformFavoriteExist)
                    {
						currentTime = day.FromTime(_gateStart);
					}
					else
					{
						currentTime = lastEndTime;
					}
                }
				else
				{
					currentTime = day.FromTime(_gateStart);
				}
				var endTime = day.FromTime(_gateEnd);
				
				while(currentTime <= endTime)
				{
					var timeslot = new TimeslotDto(day)
					{
						Start = currentTime,
						End = currentTime.AddMinutes(minutesNeeded),
						TaskType = request.TaskType,
						CompanyId = request.CompanyId == null ? Guid.Empty : request.CompanyId
					}; 
					if (timeslot.End >= endTime)
					{
						if (!platformFavoriteExist)
						{
							if (!scheduleExist)
							{
								isWrapped = true;
								lastEndTime = timeslot.End;
							}
							else
							{
								isWrapped = false;
								break;
							} 
						}
						else
						{
							break;
						}
					}
					else
					{
						isWrapped = false;
					}

					var overlapingTimeslot = await CheckForOverlaps(timeslot);
					if (overlapingTimeslot == null)
					{
						if(platformFavoriteExist)
						{
							if(await CheckForAvaliableTaskCount(request))
							{
								timeslots.Add(timeslot);
							}
						}
						else
						{
							timeslots.Add(timeslot);
						}
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
			List<Gate> gates;
			var gateSchedules = await _context.GateSchedules.ToListAsync();
			Company? company = await _context.Companies.Where(c => c.Id == timeslot.CompanyId).FirstOrDefaultAsync();
			
			var platformFavoriteExist = false;
			if(company != null)
			{
				List<PlatformFavorite>? platformFavorites = await _context.PlatformFavorites
					.Where(p => p.CompanyId == company.Id)
					.ToListAsync();

				if(platformFavorites != null)
				{
					platformFavoriteExist = true;
				}
			}
			if (!platformFavoriteExist)
			{
				List<Guid> gateIds;
				bool predicate(GateSchedule s) => s.TaskTypes.Contains(timeslot.TaskType) && s.DaysOfWeek.Contains(timeslot.Date.DayOfWeek);
				if (gateSchedules.Any(predicate))
				{
					gateIds = gateSchedules
						.Where(predicate)
						.Select(g => g.GateId)
						.ToList();
					gates = await _context.Gates
						.Include(x => x.Timeslots)
						.Include(x => x.GateSchedules)
						.Where(g => gateIds.Contains(g.Id))
						.ToListAsync();
				}
				else
				{
					gates = await _context.Gates
						.Include(x => x.Timeslots)
						.Include(x => x.GateSchedules)
						.Where(g => g.GateSchedules == null || g.GateSchedules.Count() == 0)
						.ToListAsync();
				} 
			}
			else
			{
				gates = await _context.Gates
					.Where(g => g.PlatformId == company.PlatformId)
					.Include(x => x.Timeslots)
					.ToListAsync();
			}

			

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

		private async Task<bool> CheckForAvaliableTaskCount(GetTimeslotsQuery query)
		{
			bool predicate(PlatformFavorite s) => s.TaskTypes.Contains(query.TaskType) && s.DaysOfWeek.Contains(query.Date.DayOfWeek);
			var maxTaskCount = _context.PlatformFavorites
				.Where(p => p.CompanyId == query.CompanyId)
				.Where(predicate)
				.Select(p => p.MaxTaskCount)
				.Sum();

			var company = await _context.Companies
				.Where(c => c.Id == query.CompanyId)
				.FirstOrDefaultAsync();

			var platformFavorites = company.PlatformFavorites;

			var platform = await _context.Platforms
				.Where(p => p.Id == company.PlatformId)
				.FirstOrDefaultAsync();

			var gates = await _context.Gates
				.Where(g => g.PlatformId == platform.Id)
				.Include(g => g.Timeslots)
				.ToListAsync();

			var taskCount = 0;
			foreach(var platformFavorite in platformFavorites)
			{
				foreach(var gate in gates)
				{
					foreach(var timeslot in gate.Timeslots)
					{
						if (platformFavorite.DaysOfWeek.Contains(timeslot.Date.DayOfWeek))
						{
							if(timeslot.From >= platformFavorite.From.ToDateTime(timeslot.From) && timeslot.To <= platformFavorite.To.ToDateTime(timeslot.To))
							{
								taskCount++;
							}
						}
					}
				}
			}

			return taskCount < maxTaskCount;
		}

	}
}
