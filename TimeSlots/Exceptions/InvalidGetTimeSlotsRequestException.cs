namespace TimeSlots.Exceptions
{
	public class InvalidGetTimeSlotsRequestException : Exception
	{
        public DateTime Date { get; }
		public int Pallets { get; }

		public InvalidGetTimeSlotsRequestException(DateTime date, int pallets)
		{
			Date = date;
			Pallets = pallets;
		}

		public override string Message => $"Invalid request for fetching timeslots";
	}
}
