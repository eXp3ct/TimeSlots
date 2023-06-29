using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
			timeslot.GateId = await GetFreeGateId(request, cancellationToken);

			await _context.Timeslots.AddAsync(timeslot, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
		}

		private async Task<Guid> GetFreeGateId(SetTimeslotQuery request, CancellationToken cancellationToken)
		{
			var gates = await _context.Gates.ToListAsync(cancellationToken);

			var timeslots = await _context.Timeslots
				.Where(t => t.Date == request.Date)
				.ToListAsync(cancellationToken);

			var occupiedGateIds = timeslots
				.Where(t => TimeSpan.Parse(t.From) <= TimeSpan.Parse(request.End) && TimeSpan.Parse(t.To) >= TimeSpan.Parse(request.Start))
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
