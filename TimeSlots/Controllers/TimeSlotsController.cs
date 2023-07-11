using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSlots.DataBase;
using TimeSlots.Model;
using TimeSlots.Queries;
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

		/// <summary>
		/// Получение таймслотов
		/// </summary>
		/// <remarks>
		/// Пример:
		/// POST /gettimeslots
		/// {
		///		Date: "YYYY-MM-DDTHH:mm:ss",
		///		Pallets: "int",
		///		TaskType: "int(Loading = 0, Unloading = 1, Transfer = 3)",
		///		CompanyId?: "Guid"
		/// }
		/// </remarks>
		/// <param name="query">GetTimeslotsQuery объект</param>
		/// <returns>Список свободных таймслотов</returns>
		/// <response code="200">Успешно</response>
		/// <response code="400">Неверная дата или количество паллет</response>

		[HttpPost]
		[Route("/gettimeslots")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetTimeslots([FromBody] GetTimeslotsQuery query)
		{
			if (query.Date <= DateTime.Now || query.Pallets <= 0)
				return BadRequest(new AppResultWithData<GetTimeslotsQuery>("Invalid request, check number of pallets or date", true, query));

			var timeslots = await _mediator.Send(query);

			return Ok(timeslots);
		}

		/// <summary>
		/// Бронирование таймслота
		/// </summary>
		/// <remarks>
		/// Пример:
		/// POST /settimeslots
		/// {
		///		Date: "YYYY-MM-DDTHH:mm:ss",
		///		Start: "YYYY-MM-DDTHH:mm:ss",
		///		End: "YYYY-MM-DDTHH:mm:ss",
		///		TaskType: "int(Loading = 0, Unloading = 1, Transfer = 3)",
		///		CompanyId?: "Guid"
		/// }
		/// </remarks>
		/// <param name="query">SetTimeslotQuery объект</param>
		/// <returns>AppDataWithResult объект</returns>
		/// <response code="200">Успешно</response>

		[HttpPost]
		[Route("/settimeslots")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> SetTimeslots([FromBody] SetTimeslotQuery query)
		{
			await _mediator.Send(query);

			return Ok(new AppResultWithData<SetTimeslotQuery>("Successfuly reserved timeslot", false, query));
		}

	}
}
