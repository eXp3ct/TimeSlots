using Microsoft.EntityFrameworkCore;
using TimeSlots.DataBase.Interfaces;

namespace TimeSlots.DataBase
{
    public static class DependencyInjection
	{
		public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<TimeslotsDbContext>(
				options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

			services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<TimeslotsDbContext>());

			return services;
		}
	}
}
