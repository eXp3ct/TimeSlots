﻿using Microsoft.EntityFrameworkCore;
using TimeSlots.Model;
using TimeSlots.Model.Enums;

namespace TimeSlots.DataBase.Seed
{
	public class SeedDatabase
	{

		/// <summary>
		/// Первичное создание сущностей в базу данных
		/// </summary>
		/// <remarks>
		/// 1 - платформа
		/// 3 - гейта к этой платформе
		/// 2 - расписания к двум гейтам
		/// 1 - компания
		/// 1 - расписание к компании
		/// </remarks>
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

			var company = new Company()
			{
				Id = Guid.NewGuid(),
				Name = "Company A",
				PlatformId = platform.Id,
			};

			var gateSchedule = new GateSchedule()
			{
				Id = Guid.NewGuid(),
				CompanyId = company.Id,
				DaysOfWeek = new List<DayOfWeek> { DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday},
				From = TimeSpan.Parse("12:00:00"),
				To = TimeSpan.Parse("18:00:00"),
				GateId = gate1.Id,
				TaskTypes = new List<TaskType> { TaskType.Loading, TaskType.Unloading, TaskType.Transfer},				
			};
			var gateSchedule1 = new GateSchedule()
			{
				Id = Guid.NewGuid(),
				CompanyId = null,
				DaysOfWeek = new List<DayOfWeek> { DayOfWeek.Wednesday, DayOfWeek.Thursday },
				From = TimeSpan.Parse("09:30:00"),
				To = TimeSpan.Parse("15:00:00"),
				GateId = gate3.Id,
				TaskTypes = new List<TaskType> { TaskType.Loading}
			};

			var platformFavorite = new PlatformFavorite()
			{
				Id = Guid.NewGuid(),
				PlatformId = platform.Id,
				CompanyId = company.Id,
				MaxTaskCount = random.Next(1, 11),
				DaysOfWeek = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday },
				From = TimeSpan.Parse("12:00:00"),
				To = TimeSpan.Parse("18:00:00"),
				TaskTypes = new List<TaskType> { TaskType.Unloading}
			};

			builder.Entity<Platform>().HasData(platform);
			builder.Entity<Gate>().HasData(gate1, gate2, gate3);
			builder.Entity<Company>().HasData(company);
			builder.Entity<GateSchedule>().HasData(gateSchedule, gateSchedule1);
			builder.Entity<PlatformFavorite>().HasData(platformFavorite);
		}
	}
}
