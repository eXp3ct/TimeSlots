using Microsoft.EntityFrameworkCore;
using TimeSlots.DataBase.Seed;
using TimeSlots.Model;

namespace TimeSlots.DataBase
{
	public class TimeslotsDbContext : DbContext
	{
		public DbSet<Timeslot> Timeslots { get; set; }
		public DbSet<Gate> Gates { get; set; }
		public DbSet<Platform> Platforms { get; set; }
		public DbSet<GateSchedule> GateSchedules { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<PlatformFavorite> PlatformFavorites { get; set; }

        public TimeslotsDbContext(DbContextOptions<TimeslotsDbContext> options) : base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			SeedDatabase.Seed(modelBuilder);
		}
	}
}
