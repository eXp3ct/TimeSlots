namespace TimeSlots.Model
{
	public class TimeslotDto
	{
		public DateTime Date { get; set; }
		public TimeSpan Start { get; set; }
		public TimeSpan End { get; set; }

		public TimeslotDto(DateTime date)
		{
			Date = date;
		}
	}
}
