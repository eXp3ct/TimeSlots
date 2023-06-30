using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TimeSlots;
using TimeSlots.Controllers;
using TimeSlots.DataBase;

internal class Program
{
	private static void Main(string[] args)
	{
		var host = CreateHostBuilder(args).Build();

		using var scope = host.Services.CreateScope();
		var services = scope.ServiceProvider;

		try
		{
			var context = services.GetRequiredService<TimeslotsDbContext>();
			DbInitializer.Initialize(context);
		}
		catch (Exception e)
		{
			throw new Exception("Unable to initialize database" + $"\n{e.Message}");
		}

		host.Run();
	}

	public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(webBuilder =>
			{
				webBuilder.UseStartup<Startup>();
			});
	
}