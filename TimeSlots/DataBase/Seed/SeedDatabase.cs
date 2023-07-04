using Microsoft.EntityFrameworkCore;
using TimeSlots.Model;

namespace TimeSlots.DataBase.Seed
{
	public class SeedDatabase
	{
		public static void Seed(ModelBuilder builder)
		{
			var platform = new Platform() { 
				Id = Guid.NewGuid(),
				Name = "FTC-1"
			};

			var random = new Random();

			var gate1 = new Gate()
			{
				Id = Guid.NewGuid(),
				Number = random.Next(1, 20),
				PlatformId = platform.Id,
			};
			var gate2 = new Gate()
			{
				Id = Guid.NewGuid(),
				Number = random.Next(1, 20),
				PlatformId = platform.Id
			};
			var gate3 = new Gate()
			{
				Id = Guid.NewGuid(),
				Number = random.Next(1, 20),
				PlatformId = platform.Id
			};

			builder.Entity<Platform>().HasData(platform);
			builder.Entity<Gate>().HasData(gate1, gate2, gate3);
		}
	}
}
