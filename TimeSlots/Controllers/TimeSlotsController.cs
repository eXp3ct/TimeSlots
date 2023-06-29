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
        public async Task<IActionResult> GetTimeslots(DateTime date, int pallets)
        {
            if (date.Day <= DateTime.Now.Day || pallets <= 0)
                return BadRequest(new InvalidGetTimeSlotsRequestException(date, pallets));

            var query = new GetTimeslotsQuery(date, pallets);
            var timeslots = await _mediator.Send(query);

            return Ok(timeslots);
        }

        [HttpPost]
        public async Task<IActionResult> SetTimeslots([FromBody] SetTimeslotRequestDto dto)
        {
            var query = new SetTimeslotQuery(dto.Date, dto.Start, dto.End);
            await _mediator.Send(query);

            return Ok();
        }

        public class SetTimeslotRequestDto
        {
            public DateTime Date { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
        }

        public class AppResult
        {
            public string Message { get; set; }

            public bool IsError { get; set; }
        }

        public class AppResultWithData<TData> : AppResult
        {
            public TData Data { get; set; }
        }
    }
}
