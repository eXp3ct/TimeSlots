namespace TimeSlots.Extensions
{
	public static class TimeSpanExtensions
	{
		public static TimeOnly ToTimeOnly(this TimeSpan time)
		{
			return new TimeOnly(time.Hours, time.Minutes, time.Seconds);
		}

		public static DateTime ToDateTime(this TimeSpan time, DateTime day)
		{
			return new DateTime(day.Year, day.Month, day.Day, time.Hours, time.Minutes, time.Seconds);
		}
	}
}
