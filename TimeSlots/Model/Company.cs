using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSlots.Model
{
	public class Company
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid PlatformId { get; set; }
		public virtual GateSchedule GateSchedule { get; set; }
		public virtual List<PlatformFavorite> PlatformFavorites { get; set; }
	}
}
