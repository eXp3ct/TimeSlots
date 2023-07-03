namespace TimeSlots.Extensions
{
	public static class DateTimeExtensions
	{
		public static DateTime FromTime(this DateTime date, TimeOnly time)
		{
			return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
		}
	}
}
