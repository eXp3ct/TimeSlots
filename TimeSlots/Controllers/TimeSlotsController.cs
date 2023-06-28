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
		private readonly IMediator _mediator;

		public TimeSlotsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult<IList<TimeslotDto>>> GetTimeslots(DateTime date, int pallets)
		{
			if (date.Day <= DateTime.Now.Day || pallets <= 0)
				return BadRequest(new InvalidGetTimeSlotsRequestException(date, pallets));

			var query = new GetTimeslotsQuery(date, pallets);
			var timeslots = await _mediator.Send(query);

			return Ok(timeslots);
		}

		[HttpPost]
		public async Task<IActionResult> SetTimeslots(DateTime date, string start, string end)
		{
			var query = new SetTimeslotQuery(date, start, end);
			await _mediator.Send(query);

			return Ok();
		}
	}
}
