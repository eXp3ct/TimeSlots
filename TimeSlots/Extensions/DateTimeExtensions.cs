namespace TimeSlots.Extensions
{
	public static class DateTimeExtensions
	{
		/// <summary>
		/// Метод создания DateTime из TimeOnly
		/// </summary>
		/// <param name="date">Дата</param>
		/// <param name="time">Время</param>
		/// <returns>DateTime по дате и вермени</returns>
		public static DateTime FromTime(this DateTime date, TimeOnly time)
		{
			return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
		}
	}
}
