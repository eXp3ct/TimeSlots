using Humanizer;
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
		private TimeOnly _gateStart = new(0, 0, 0);
		private TimeOnly _gateEnd = new(23, 30, 0);

		public GetTimeslotsQueryHandler(TimeslotsDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<TimeslotDto>> Handle(GetTimeslotsQuery request, CancellationToken cancellationToken)
		{
			Company? company = await _context.Companies.Where(c => c.Id == request.CompanyId).FirstOrDefaultAsync(cancellationToken);
			List<PlatformFavorite>? platformFavorites = await _context.PlatformFavorites.ToListAsync(cancellationToken);
			if(company != null)
			{
				platformFavorites = platformFavorites.Where(p => p.CompanyId == company.Id).ToList();
			}
			var timeslots = new List<TimeslotDto>();
			var nearbyDates = await EnqueueNearbyDates(request.Date);

			var minutesNeeded = GetNeededMinutes(request.Pallets);
			var isWrapped = false;
			var lastEndTime = DateTime.MinValue;
			var schedules = await _context.GateSchedules.ToListAsync(cancellationToken);
			foreach(var day in  nearbyDates)
			{
				var scheduleExist = false;
				var platformFavoriteExist = false;
				if (platformFavorites == null)
				{
					bool predicate(GateSchedule s) => s.DaysOfWeek.Contains(day.Date.DayOfWeek) && s.TaskTypes.Contains(request.TaskType);
					
					if (schedules.Any(predicate))
					{
						scheduleExist = true;
						var schedule = schedules.Where(predicate).FirstOrDefault();
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
					bool predicate(PlatformFavorite p) => p.DaysOfWeek.Contains(day.Date.DayOfWeek) && p.TaskTypes.Contains(request.TaskType);

					if (platformFavorites.Any(predicate))
					{
						platformFavoriteExist = true;
						var platformFavorite = platformFavorites.Where(predicate).FirstOrDefault();
						_gateStart = platformFavorite.From.ToTimeOnly();
						_gateEnd = platformFavorite.To.ToTimeOnly();
					}
					else
					{
						platformFavoriteExist = false;
						_gateStart = new(0, 0, 0);
						_gateEnd = new(23, 30, 0);
					}
				}
				var currentTime = isWrapped ? lastEndTime : day.FromTime(_gateStart);
				var endTime = day.FromTime(_gateEnd);
				
				while(currentTime <= endTime)
				{
					var timeslot = new TimeslotDto(day)
					{
						Start = currentTime,
						End = currentTime.AddMinutes(minutesNeeded),
						TaskType = request.TaskType,
						CompanyId = request.CompanyId == null ? Guid.Empty : request.CompanyId,
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
				.Include(p => p.Gates)
				.FirstOrDefaultAsync();
			var taskCount = 0;
			foreach(var platformFavorite in platformFavorites)
			{
				foreach(var gate in platform.Gates)
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
