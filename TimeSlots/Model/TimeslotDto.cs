using TimeSlots.Model.Enums;

namespace TimeSlots.Model
{
	public class TimeslotDto
	{
		public DateTime Date { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public TaskType TaskType { get; set; }

		public TimeslotDto(DateTime date)
		{
			Date = date;
		}
	}
}
