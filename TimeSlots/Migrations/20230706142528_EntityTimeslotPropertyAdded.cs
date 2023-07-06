using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimeSlots.Migrations
{
    /// <inheritdoc />
    public partial class EntityTimeslotPropertyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "TaskType",
                table: "Timeslots",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "TaskType",
                table: "Timeslots");

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
    }
}
