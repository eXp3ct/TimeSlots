using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using TimeSlots.DataBase;
using TimeSlots.Model;

namespace TimeSlots.Queries
{
	public class SetTimeslotQueryHandler : IRequestHandler<SetTimeslotQuery>
	{
		private readonly TimeslotsDbContext _context;
		private readonly IMapper _mapper;

		public SetTimeslotQueryHandler(TimeslotsDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task Handle(SetTimeslotQuery request, CancellationToken cancellationToken)
		{
			var timeslot = _mapper.Map<Timeslot>(request);
			var gateId = await GetFreeGateId(request, cancellationToken);
			if (gateId != Guid.Empty)
				timeslot.GateId = gateId;
			else
				return;
			await _context.Timeslots.AddAsync(timeslot, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
		}

		private async Task<Guid> GetFreeGateId(SetTimeslotQuery request, CancellationToken cancellationToken)
		{
			List<Gate> gates;

			Company? company = await _context.Companies.Where(c => c.Id == request.CompanyId).FirstOrDefaultAsync(cancellationToken);

			var platformFavoriteExist = false;
			if (company != null)
			{
				List<PlatformFavorite>? platformFavorites = await _context.PlatformFavorites
					.Where(p => p.CompanyId == company.Id)
					.ToListAsync();

				if (platformFavorites != null)
				{
					platformFavoriteExist = true;
				}
			}
			if (!platformFavoriteExist)
			{
				gates = await _context.Gates
						.Include(g => g.GateSchedules)
						.ToListAsync(cancellationToken);
				var gateSchedules = await _context.GateSchedules.ToListAsync(cancellationToken);
				List<Guid> gateIds;
				bool predicate(GateSchedule s) => s.TaskTypes.Contains(request.TaskType) && s.DaysOfWeek.Contains(request.Date.DayOfWeek);
				if (gateSchedules.Any(predicate))
				{
					gateIds = gateSchedules
						.Where(predicate)
						.Select(g => g.GateId).ToList();
					gates = gates.Where(g => gateIds.Contains(g.Id)).ToList();
				}
				else
				{
					gates = gates.Where(g => g.GateSchedules == null || g.GateSchedules.Count() == 0).ToList();
				} 
			}
			else
			{
				gates = await _context.Gates
					.Where(g => g.PlatformId == company.PlatformId)
					.ToListAsync(cancellationToken);
			}
			var timeslots = await _context.Timeslots
				.Where(t => t.Date.Date == request.Date.Date)
				.ToListAsync(cancellationToken);

			var occupiedGateIds = timeslots
				.Where(t => t.From < request.End && t.To > request.Start)
				.Select(t => t.GateId)
				.ToList();

			var availableGates = gates.Where(g => !occupiedGateIds.Contains(g.Id)).ToList();
			if (availableGates.Count == 0)
			{
				// Нет доступных гейтов в указанное время

				return Guid.Empty;
			}

			var random = new Random();
			var randomGate = availableGates[random.Next(0, availableGates.Count)];

			return randomGate.Id;
		}
	}
}
