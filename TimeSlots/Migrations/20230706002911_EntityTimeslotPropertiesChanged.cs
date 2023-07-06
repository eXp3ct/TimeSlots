using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimeSlots.Migrations
{
    /// <inheritdoc />
    public partial class EntityTimeslotPropertiesChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GateSchedules",
                keyColumn: "Id",
                keyValue: new Guid("5a218786-1c90-4296-9219-c91a735fad83"));

            migrationBuilder.DeleteData(
                table: "Gates",
                keyColumn: "Id",
                keyValue: new Guid("49801c31-3f5e-4b4b-acf4-c575bb3c4418"));

            migrationBuilder.DeleteData(
                table: "Gates",
                keyColumn: "Id",
                keyValue: new Guid("52340c69-51d7-467b-be1f-885850902380"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("a28f6e43-97fb-4015-bd58-d07818e83796"));

            migrationBuilder.DeleteData(
                table: "Gates",
                keyColumn: "Id",
                keyValue: new Guid("ff6bf823-768c-47ac-9d1c-c2b9cc41af5d"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("8de9fe12-3c5c-4e89-b76a-541c7c212ffd"));

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("72fc8c4c-45b5-4a1e-aaff-9cb8dfea9796"), "FTC-1" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name", "PlatformId" },
                values: new object[] { new Guid("86a9a685-09f0-4d99-998e-4fa4af6d4f82"), "Company A", new Guid("72fc8c4c-45b5-4a1e-aaff-9cb8dfea9796") });

            migrationBuilder.InsertData(
                table: "Gates",
                columns: new[] { "Id", "Number", "PlatformId" },
                values: new object[,]
                {
                    { new Guid("0c47ee63-4c54-4881-8938-4ed30eb56699"), 5, new Guid("72fc8c4c-45b5-4a1e-aaff-9cb8dfea9796") },
                    { new Guid("43255c1e-172a-4167-b3c4-763f2835cfd8"), 4, new Guid("72fc8c4c-45b5-4a1e-aaff-9cb8dfea9796") },
                    { new Guid("96a21fcb-67b9-4768-b8c2-9664d7b7823b"), 18, new Guid("72fc8c4c-45b5-4a1e-aaff-9cb8dfea9796") }
                });

            migrationBuilder.InsertData(
                table: "GateSchedules",
                columns: new[] { "Id", "CompanyId", "DaysOfWeekString", "From", "GateId", "TaskTypesString", "To" },
                values: new object[] { new Guid("4eb080aa-48a7-4863-a690-eda2fcb02b33"), new Guid("86a9a685-09f0-4d99-998e-4fa4af6d4f82"), "Sunday,Monday,Tuesday", "12:00:00", new Guid("0c47ee63-4c54-4881-8938-4ed30eb56699"), "Loading,Unloading,Transfer", "18:00:00" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GateSchedules",
                keyColumn: "Id",
                keyValue: new Guid("4eb080aa-48a7-4863-a690-eda2fcb02b33"));

            migrationBuilder.DeleteData(
                table: "Gates",
                keyColumn: "Id",
                keyValue: new Guid("43255c1e-172a-4167-b3c4-763f2835cfd8"));

            migrationBuilder.DeleteData(
                table: "Gates",
                keyColumn: "Id",
                keyValue: new Guid("96a21fcb-67b9-4768-b8c2-9664d7b7823b"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("86a9a685-09f0-4d99-998e-4fa4af6d4f82"));

            migrationBuilder.DeleteData(
                table: "Gates",
                keyColumn: "Id",
                keyValue: new Guid("0c47ee63-4c54-4881-8938-4ed30eb56699"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("72fc8c4c-45b5-4a1e-aaff-9cb8dfea9796"));

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("8de9fe12-3c5c-4e89-b76a-541c7c212ffd"), "FTC-1" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name", "PlatformId" },
                values: new object[] { new Guid("a28f6e43-97fb-4015-bd58-d07818e83796"), "Company A", new Guid("8de9fe12-3c5c-4e89-b76a-541c7c212ffd") });

            migrationBuilder.InsertData(
                table: "Gates",
                columns: new[] { "Id", "Number", "PlatformId" },
                values: new object[,]
                {
                    { new Guid("49801c31-3f5e-4b4b-acf4-c575bb3c4418"), 8, new Guid("8de9fe12-3c5c-4e89-b76a-541c7c212ffd") },
                    { new Guid("52340c69-51d7-467b-be1f-885850902380"), 3, new Guid("8de9fe12-3c5c-4e89-b76a-541c7c212ffd") },
                    { new Guid("ff6bf823-768c-47ac-9d1c-c2b9cc41af5d"), 4, new Guid("8de9fe12-3c5c-4e89-b76a-541c7c212ffd") }
                });

            migrationBuilder.InsertData(
                table: "GateSchedules",
                columns: new[] { "Id", "CompanyId", "DaysOfWeekString", "From", "GateId", "TaskTypesString", "To" },
                values: new object[] { new Guid("5a218786-1c90-4296-9219-c91a735fad83"), new Guid("a28f6e43-97fb-4015-bd58-d07818e83796"), "Sunday,Monday,Tuesday", "12:00:00", new Guid("ff6bf823-768c-47ac-9d1c-c2b9cc41af5d"), "Loading,Unloading,Transfer", "18:00:00" });
        }
    }
}
