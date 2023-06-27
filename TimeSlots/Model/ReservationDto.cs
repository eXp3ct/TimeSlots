namespace TimeSlots.Model
{
	public class ReservationDto
	{
		public DateTime Date { get; set; }
		public string Start { get; set; }
		public string End { get; set; }

		public ReservationDto(DateTime date, string start, string end)
		{
			Date = date;
			Start = start;
			End = end;
		}
	}
}
