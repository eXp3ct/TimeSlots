namespace TimeSlots.Extensions
{
	public static class TimeSpanExtensions
	{
		/// <summary>
		/// Создание TimeOnly из TimeSpan
		/// </summary>
		/// <param name="time"></param>
		/// <returns>TimeOnly с указанным временем</returns>
		public static TimeOnly ToTimeOnly(this TimeSpan time)
		{
			return new TimeOnly(time.Hours, time.Minutes, time.Seconds);
		}

		/// <summary>
		/// Создание DateTime по указанному времени
		/// </summary>
		/// <param name="time"></param>
		/// <param name="day"></param>
		/// <returns>DateTime с указанным временем и датой</returns>
		public static DateTime ToDateTime(this TimeSpan time, DateTime day)
		{
			return new DateTime(day.Year, day.Month, day.Day, time.Hours, time.Minutes, time.Seconds);
		}
	}
}
