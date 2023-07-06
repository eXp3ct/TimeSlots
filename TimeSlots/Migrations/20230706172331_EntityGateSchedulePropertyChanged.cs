using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimeSlots.Migrations
{
    /// <inheritdoc />
    public partial class EntityGateSchedulePropertyChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GateSchedules",
                keyColumn: "Id",
                keyValue: new Guid("7bb2b061-fd79-4622-a7a3-ce97a3e4a576"));

            migrationBuilder.DeleteData(
                table: "Gates",
                keyColumn: "Id",
                keyValue: new Guid("9ac7306d-1979-4ade-abaa-7b157702fdca"));

            migrationBuilder.DeleteData(
                table: "Gates",
                keyColumn: "Id",
                keyValue: new Guid("cb7d29f1-6307-4b79-aba7-ad74d5798004"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("0cea26e2-bfa5-48d9-9ac3-69f5b697f43f"));

            migrationBuilder.DeleteData(
                table: "Gates",
                keyColumn: "Id",
                keyValue: new Guid("aed76816-a08d-486f-9f22-f671eca2767f"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("6753ddab-c262-4528-ace6-c6471d20aef6"));

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("cf464a7a-7804-45c7-89bf-019855e3da8a"), "FTC-1" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name", "PlatformId" },
                values: new object[] { new Guid("61ab4269-2957-4fbb-b56a-ec18610fa77a"), "Company A", new Guid("cf464a7a-7804-45c7-89bf-019855e3da8a") });

            migrationBuilder.InsertData(
                table: "Gates",
                columns: new[] { "Id", "Number", "PlatformId" },
                values: new object[,]
                {
                    { new Guid("594fabaa-65ca-4940-834f-9d31ad4b4d0d"), 17, new Guid("cf464a7a-7804-45c7-89bf-019855e3da8a") },
                    { new Guid("dd96e026-686c-4008-b035-d4220f6e30ac"), 9, new Guid("cf464a7a-7804-45c7-89bf-019855e3da8a") },
                    { new Guid("e755977d-efa1-412e-a581-ac9a95bc494f"), 1, new Guid("cf464a7a-7804-45c7-89bf-019855e3da8a") }
                });

            migrationBuilder.InsertData(
                table: "GateSchedules",
                columns: new[] { "Id", "CompanyId", "DaysOfWeekString", "From", "GateId", "TaskTypesString", "To" },
                values: new object[,]
                {
                    { new Guid("cb211e29-e305-4d87-8d12-ca346a290cab"), null, "Wednesday,Thursday", new TimeSpan(0, 9, 30, 0, 0), new Guid("dd96e026-686c-4008-b035-d4220f6e30ac"), "Loading", new TimeSpan(0, 15, 0, 0, 0) },
                    { new Guid("fb05f4b5-23ca-4ba7-a05b-caaf3efb2129"), new Guid("61ab4269-2957-4fbb-b56a-ec18610fa77a"), "Sunday,Monday,Tuesday", new TimeSpan(0, 12, 0, 0, 0), new Guid("594fabaa-65ca-4940-834f-9d31ad4b4d0d"), "Loading,Unloading,Transfer", new TimeSpan(0, 18, 0, 0, 0) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GateSchedules",
                keyColumn: "Id",
                keyValue: new Guid("cb211e29-e305-4d87-8d12-ca346a290cab"));

            migrationBuilder.DeleteData(
                table: "GateSchedules",
                keyColumn: "Id",
                keyValue: new Guid("fb05f4b5-23ca-4ba7-a05b-caaf3efb2129"));

            migrationBuilder.DeleteData(
                table: "Gates",
                keyColumn: "Id",
                keyValue: new Guid("e755977d-efa1-412e-a581-ac9a95bc494f"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("61ab4269-2957-4fbb-b56a-ec18610fa77a"));

            migrationBuilder.DeleteData(
                table: "Gates",
                keyColumn: "Id",
                keyValue: new Guid("594fabaa-65ca-4940-834f-9d31ad4b4d0d"));

            migrationBuilder.DeleteData(
                table: "Gates",
                keyColumn: "Id",
                keyValue: new Guid("dd96e026-686c-4008-b035-d4220f6e30ac"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("cf464a7a-7804-45c7-89bf-019855e3da8a"));

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("6753ddab-c262-4528-ace6-c6471d20aef6"), "FTC-1" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name", "PlatformId" },
                values: new object[] { new Guid("0cea26e2-bfa5-48d9-9ac3-69f5b697f43f"), "Company A", new Guid("6753ddab-c262-4528-ace6-c6471d20aef6") });

            migrationBuilder.InsertData(
                table: "Gates",
                columns: new[] { "Id", "Number", "PlatformId" },
                values: new object[,]
                {
                    { new Guid("9ac7306d-1979-4ade-abaa-7b157702fdca"), 13, new Guid("6753ddab-c262-4528-ace6-c6471d20aef6") },
                    { new Guid("aed76816-a08d-486f-9f22-f671eca2767f"), 3, new Guid("6753ddab-c262-4528-ace6-c6471d20aef6") },
                    { new Guid("cb7d29f1-6307-4b79-aba7-ad74d5798004"), 12, new Guid("6753ddab-c262-4528-ace6-c6471d20aef6") }
                });

            migrationBuilder.InsertData(
                table: "GateSchedules",
                columns: new[] { "Id", "CompanyId", "DaysOfWeekString", "From", "GateId", "TaskTypesString", "To" },
                values: new object[] { new Guid("7bb2b061-fd79-4622-a7a3-ce97a3e4a576"), new Guid("0cea26e2-bfa5-48d9-9ac3-69f5b697f43f"), "Sunday,Monday,Tuesday", "12:00:00", new Guid("aed76816-a08d-486f-9f22-f671eca2767f"), "Loading,Unloading,Transfer", "18:00:00" });
        }
    }
}
