using Microsoft.EntityFrameworkCore;
using TimeSlots.Model;

namespace TimeSlots.DataBase
{
	public class TimeslotsDbContext : DbContext
	{
		public DbSet<Timeslot> Timeslots { get; set; }
		public DbSet<Gate> Gates { get; set; }
		public DbSet<Platform> Platforms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=timeslots.db");
		}
	}
}
