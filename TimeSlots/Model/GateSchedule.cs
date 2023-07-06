using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeSlots.Model.Enums;

namespace TimeSlots.Model
{
	public class GateSchedule
	{
		public Guid Id { get; set; }

		// Строка для хранения дней недели через разделитель (например, запятую)
		public string DaysOfWeekString { get; set; }

		[NotMapped]
		public List<DayOfWeek> DaysOfWeek
		{
			get => DaysOfWeekString?.Split(',').Select(x => Enum.Parse<DayOfWeek>(x)).ToList() ?? new List<DayOfWeek>();
			set => DaysOfWeekString = string.Join(",", value.Select(x => x.ToString()));
		}

		public TimeSpan From { get; set; }
		public TimeSpan To { get; set; }
		public Guid GateId { get; set; }
		public Guid? CompanyId { get; set; } = null;

		// Строка для хранения типов задач через разделитель (например, запятую)
		public string TaskTypesString { get; set; }

		[NotMapped]
		public List<TaskType> TaskTypes
		{
			get => TaskTypesString?.Split(',').Select(x => Enum.Parse<TaskType>(x)).ToList() ?? new List<TaskType>();
			set => TaskTypesString = string.Join(",", value.Select(x => x.ToString()));
		}
	}
}
