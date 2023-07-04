namespace TimeSlots.Model
{
	public class Timeslot
	{
		public Guid Id { get; set; }
		public DateTime Date { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public Guid GateId { get; set; }
		public Guid UserId { get; set; }

		public virtual Gate Gate { get; set; }
	}
}
