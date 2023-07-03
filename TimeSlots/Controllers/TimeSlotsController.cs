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

		[HttpPost]
		[Route("/gettimeslots")]
		public async Task<IActionResult> GetTimeslots([FromBody] GetTimeslotsQuery query)
		{
			if (query.Date <= DateTime.Now || query.Pallets <= 0)
				return BadRequest(new AppResultWithData<GetTimeslotsQuery>("Invalid request, check number of pallets or date", true, query));

			var timeslots = await _mediator.Send(query);

			return Ok(timeslots);
		}

		[HttpPost]
		[Route("/settimeslots")]
		public async Task<IActionResult> SetTimeslots([FromBody] SetTimeslotQuery query)
		{
			await _mediator.Send(query);

			return Ok(new AppResultWithData<SetTimeslotQuery>("Successfuly reserved timeslot", false, query));
		}
	}
}
