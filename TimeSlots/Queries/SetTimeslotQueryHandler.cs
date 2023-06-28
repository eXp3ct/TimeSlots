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
			var timeslot = new Timeslot()
			{
				Id = Guid.NewGuid(),
				Date = request.Date,
				From = request.Start,
				To = request.End,
				GateId = (await _context.Gates.ToListAsync(cancellationToken))[new Random().Next(0, _context.Gates.Count())].Id,
				UserId = Guid.NewGuid()
			};

			await _context.Timeslots.AddAsync(timeslot, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
