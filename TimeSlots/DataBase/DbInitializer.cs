namespace TimeSlots.DataBase
{
	public class DbInitializer
	{

		/// <summary>
		/// Инициализация базы данных
		/// </summary>
		/// <param name="context">Контекст базы данных</param>
		public static void Initialize(TimeslotsDbContext context)
		{
			context.Database.EnsureCreated();
		}
	}
}
