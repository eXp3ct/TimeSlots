using Microsoft.AspNetCore.Mvc;
using TimeSlots.DataBase;
using TimeSlots.Model;

namespace TimeSlots.Controllers
{
	[Route("[controller]")]
	public class TimeSlotsController : Controller
	{
		private readonly TimeslotsDbContext _context;

		public TimeSlotsController(TimeslotsDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public ActionResult<IList<Timeslot>> Get()
		{
			return _context.Timeslots.ToList();
		}

		[HttpPost]
		public ActionResult<Timeslot> Create(Timeslot timeslot)
		{

			_context.Timeslots.Add(timeslot);
			_context.SaveChanges();

			return CreatedAtAction(nameof(Get), new { id = timeslot.Id });
		}
	}
}
