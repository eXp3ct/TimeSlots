namespace TimeSlots.Model
{
	public class Platform
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public List<Gate> Gates { get; set; }
	}
}
