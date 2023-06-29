using MediatR;
using Microsoft.EntityFrameworkCore;
using TimeSlots.DataBase;
using TimeSlots.Model;

namespace TimeSlots.Queries
{
	public class SetTimeslotQueryHandler : IRequestHandler<SetTimeslotQuery>
	{
		private readonly TimeslotsDbContext _context;

		public SetTimeslotQueryHandler(TimeslotsDbContext context)
		{
			_context = context;
		}

		public async Task Handle(SetTimeslotQuery request, CancellationToken cancellationToken)
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
				
				return;
			}

			var random = new Random();
			var randomGate = availableGates[random.Next(0, availableGates.Count)];

			var timeslot = new Timeslot()
			{
				Id = Guid.NewGuid(),
				Date = request.Date,
				From = request.Start,
				To = request.End,
				GateId = randomGate.Id,
				UserId = Guid.NewGuid()
			};

			await _context.Timeslots.AddAsync(timeslot, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
		}

	}
}
