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
		//var builder = WebApplication.CreateBuilder(args);

		//// Add services to the container.

		//builder.Services.AddControllers();
		//builder.Services.AddDbContext<TimeslotsDbContext>();
		//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		//builder.Services.AddEndpointsApiExplorer();
		//builder.Services.AddSwaggerGen();

		//var app = builder.Build();

		//// Configure the HTTP request pipeline.
		//if (app.Environment.IsDevelopment())
		//{
		//	app.UseSwagger();
		//	app.UseSwaggerUI();
		//}

		//app.UseHttpsRedirection();

		//app.UseAuthorization();

		//app.MapControllers();

		//app.Run();

		CreateHostBuilder(args).Build().Run();
	}

	public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(webBuilder =>
			{
				webBuilder.UseStartup<Startup>();
			});
	
}