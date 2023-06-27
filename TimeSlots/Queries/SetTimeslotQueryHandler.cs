using MediatR;
using Microsoft.EntityFrameworkCore;
using TimeSlots.DataBase;
using TimeSlots.Model;

namespace TimeSlots.Queries
{
	public class SetTimeslotQueryHandler : IRequestHandler<SetTimeslotQuery>
	{
		private readonly TimeslotsDbContext _context;
		private readonly ILogger<SetTimeslotQueryHandler> _logger;	

		public SetTimeslotQueryHandler(TimeslotsDbContext context, ILogger<SetTimeslotQueryHandler> logger)
		{
			_context = context;
			_logger = logger;
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

			string message = $"Timeslot {timeslot.Id} seems like added to DB";
			_logger.LogInformation(message, cancellationToken);
		}
	}
}
