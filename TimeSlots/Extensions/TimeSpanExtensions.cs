namespace TimeSlots.Extensions
{
	public static class TimeSpanExtensions
	{
		public static TimeOnly ToTimeOnly(this TimeSpan time)
		{
			return new TimeOnly(time.Hours, time.Minutes, time.Seconds);
		}
	}
}
