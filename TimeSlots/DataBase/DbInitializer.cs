namespace TimeSlots.DataBase
{
	public class DbInitializer
	{
		public static void Initialize(TimeslotsDbContext context)
		{
			context.Database.EnsureCreated();
		}
	}
}
