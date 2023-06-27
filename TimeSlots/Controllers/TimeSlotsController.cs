using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSlots.DataBase;
using TimeSlots.Model;
using TimeSlots.Queries;
using TimeSlots.Exceptions;
namespace TimeSlots.Controllers
{
	[Route("[controller]")]
	public class TimeSlotsController : Controller
	{
		private readonly TimeslotsDbContext _context;
		private readonly ILogger<TimeSlotsController> _logger;
		private readonly IMediator _mediator;

		public TimeSlotsController(TimeslotsDbContext context, ILogger<TimeSlotsController> logger, IMediator mediator)
		{
			_context = context;
			_logger = logger;
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult<IList<TimeslotDto>>> Get(DateTime date, int pallets)
		{
			if (date.Day <= DateTime.Now.Day || pallets <= 0)
				return BadRequest(new InvalidGetTimeSlotsRequestException(date, pallets));

			var query = new GetTimeslotsQuery(date, pallets);
			var timeslots = await _mediator.Send(query);

			return Ok(timeslots);
		}

		[HttpPost]
		public async Task<IActionResult> Create(Timeslot timeslot)
		{
			timeslot.Id = Guid.NewGuid();
			timeslot.UserId = Guid.NewGuid();
			timeslot.GateId = Guid.NewGuid();

			await _context.Timeslots.AddAsync(timeslot);
			await _context.SaveChangesAsync();

			_logger.LogInformation($"Stored {timeslot.Id} timeslots in timeslots.db");

			return CreatedAtAction(nameof(Get), new { id = timeslot.Id }, timeslot);
		}
	}
}
