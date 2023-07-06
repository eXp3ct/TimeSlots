﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeSlots.DataBase;

#nullable disable

namespace TimeSlots.Migrations
{
    [DbContext(typeof(TimeslotsDbContext))]
    partial class TimeslotsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.8");

            modelBuilder.Entity("TimeSlots.Model.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PlatformId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PlatformId");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("61ab4269-2957-4fbb-b56a-ec18610fa77a"),
                            Name = "Company A",
                            PlatformId = new Guid("cf464a7a-7804-45c7-89bf-019855e3da8a")
                        });
                });

            modelBuilder.Entity("TimeSlots.Model.Gate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("PlatformId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PlatformId");

                    b.ToTable("Gates");

                    b.HasData(
                        new
                        {
                            Id = new Guid("594fabaa-65ca-4940-834f-9d31ad4b4d0d"),
                            Number = 17,
                            PlatformId = new Guid("cf464a7a-7804-45c7-89bf-019855e3da8a")
                        },
                        new
                        {
                            Id = new Guid("e755977d-efa1-412e-a581-ac9a95bc494f"),
                            Number = 1,
                            PlatformId = new Guid("cf464a7a-7804-45c7-89bf-019855e3da8a")
                        },
                        new
                        {
                            Id = new Guid("dd96e026-686c-4008-b035-d4220f6e30ac"),
                            Number = 9,
                            PlatformId = new Guid("cf464a7a-7804-45c7-89bf-019855e3da8a")
                        });
                });

            modelBuilder.Entity("TimeSlots.Model.GateSchedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("TEXT");

                    b.Property<string>("DaysOfWeekString")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("From")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GateId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TaskTypesString")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("To")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId")
                        .IsUnique();

                    b.HasIndex("GateId");

                    b.ToTable("GateSchedules");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fb05f4b5-23ca-4ba7-a05b-caaf3efb2129"),
                            CompanyId = new Guid("61ab4269-2957-4fbb-b56a-ec18610fa77a"),
                            DaysOfWeekString = "Sunday,Monday,Tuesday",
                            From = new TimeSpan(0, 12, 0, 0, 0),
                            GateId = new Guid("594fabaa-65ca-4940-834f-9d31ad4b4d0d"),
                            TaskTypesString = "Loading,Unloading,Transfer",
                            To = new TimeSpan(0, 18, 0, 0, 0)
                        },
                        new
                        {
                            Id = new Guid("cb211e29-e305-4d87-8d12-ca346a290cab"),
                            DaysOfWeekString = "Wednesday,Thursday",
                            From = new TimeSpan(0, 9, 30, 0, 0),
                            GateId = new Guid("dd96e026-686c-4008-b035-d4220f6e30ac"),
                            TaskTypesString = "Loading",
                            To = new TimeSpan(0, 15, 0, 0, 0)
                        });
                });

            modelBuilder.Entity("TimeSlots.Model.Platform", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Platforms");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cf464a7a-7804-45c7-89bf-019855e3da8a"),
                            Name = "FTC-1"
                        });
                });

            modelBuilder.Entity("TimeSlots.Model.Timeslot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("From")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GateId")
                        .HasColumnType("TEXT");

                    b.Property<int>("TaskType")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("To")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GateId");

                    b.ToTable("Timeslots");
                });

            modelBuilder.Entity("TimeSlots.Model.Company", b =>
                {
                    b.HasOne("TimeSlots.Model.Platform", null)
                        .WithMany("Companies")
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TimeSlots.Model.Gate", b =>
                {
                    b.HasOne("TimeSlots.Model.Platform", null)
                        .WithMany("Gates")
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TimeSlots.Model.GateSchedule", b =>
                {
                    b.HasOne("TimeSlots.Model.Company", null)
                        .WithOne("GateSchedule")
                        .HasForeignKey("TimeSlots.Model.GateSchedule", "CompanyId");

                    b.HasOne("TimeSlots.Model.Gate", null)
                        .WithMany("GateSchedules")
                        .HasForeignKey("GateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TimeSlots.Model.Timeslot", b =>
                {
                    b.HasOne("TimeSlots.Model.Gate", null)
                        .WithMany("Timeslots")
                        .HasForeignKey("GateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TimeSlots.Model.Company", b =>
                {
                    b.Navigation("GateSchedule")
                        .IsRequired();
                });

            modelBuilder.Entity("TimeSlots.Model.Gate", b =>
                {
                    b.Navigation("GateSchedules");

                    b.Navigation("Timeslots");
                });

            modelBuilder.Entity("TimeSlots.Model.Platform", b =>
                {
                    b.Navigation("Companies");

                    b.Navigation("Gates");
                });
#pragma warning restore 612, 618
        }
    }
}
