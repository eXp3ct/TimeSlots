using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using TimeSlots.DataBase;
using TimeSlots.Mapping;

namespace TimeSlots
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
		{
			if (environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapRazorPages();
			});
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();
			services.AddControllers();
			services.AddDbContext<TimeslotsDbContext>(
				options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"))
				);
			services.AddMvc();
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
		}
	}
}
