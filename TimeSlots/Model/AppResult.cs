namespace TimeSlots.Model
{
	public class AppResult
	{
		public string Message { get; set; }
		public bool IsError { get; set; }
		public DateTime Date { get; set; }

		public AppResult(string message, bool isError)
		{
			Message = message;
			IsError = isError;
			Date = DateTime.UtcNow;
		}
	}
}
