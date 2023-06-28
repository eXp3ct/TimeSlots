using MediatR;
using TimeSlots.Model;

namespace TimeSlots.Queries
{
	public class SetTimeslotQuery : IRequest
	{
		public DateTime Date { get; set; }
		public string Start { get; set; }
		public string End { get; set; }

		public SetTimeslotQuery(DateTime date, string start, string end)
		{
			Date = date;
			Start = start;
			End = end;
		}
	}
}
