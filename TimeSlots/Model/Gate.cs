namespace TimeSlots.Model
{
	public class Gate
	{
		public Guid Id { get; set; }
		public int Number { get; set; }
		public Guid PlatformId { get; set; }

		public virtual List<Timeslot> Timeslots { get; set; }
		public virtual List<GateSchedule> GateSchedules { get; set; }
	}
}
