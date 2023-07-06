using TimeSlots.Model.Enums;

namespace TimeSlots.Model
{
	public class Timeslot
	{
		public Guid Id { get; set; }
		public DateTime Date { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }
		public Guid GateId { get; set; }
		public Guid UserId { get; set; }
		public TaskType TaskType { get; set; }
	}
}
