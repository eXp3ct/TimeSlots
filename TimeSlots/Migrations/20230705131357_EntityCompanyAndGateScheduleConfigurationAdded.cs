using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimeSlots.Migrations
{
    /// <inheritdoc />
    public partial class EntityCompanyAndGateScheduleConfigurationAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Gates",
                keyColumn: "Id",
                keyValue: new Guid("14338de4-7ed1-4e78-8283-eb1b87060263"));

            migrationBuilder.DeleteData(
                table: "Gates",
                keyColumn: "Id",
                keyValue: new Guid("7c846671-27f5-4127-8a28-ac174d34fc86"));

            migrationBuilder.DeleteData(
                table: "Gates",
                keyColumn: "Id",
                keyValue: new Guid("ccca44cb-bdb5-4b9c-abc9-87edd970a81d"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("c7108a75-79ac-46e8-8208-e19d6fe8a252"));

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PlatformId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GateSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DaysOfWeekString = table.Column<string>(type: "TEXT", nullable: false),
                    From = table.Column<string>(type: "TEXT", nullable: false),
                    To = table.Column<string>(type: "TEXT", nullable: false),
                    GateId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CompanyId = table.Column<Guid>(type: "TEXT", nullable: true),
                    TaskTypesString = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GateSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GateSchedules_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GateSchedules_Gates_GateId",
                        column: x => x.GateId,
                        principalTable: "Gates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Companies_PlatformId",
                table: "Companies",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_GateSchedules_CompanyId",
                table: "GateSchedules",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GateSchedules_GateId",
                table: "GateSchedules",
                column: "GateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GateSchedules");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DeleteData(
                table: "Gates",
                keyColumn: "Id",
                keyValue: new Guid("49801c31-3f5e-4b4b-acf4-c575bb3c4418"));

            migrationBuilder.DeleteData(
                table: "Gates",
                keyColumn: "Id",
                keyValue: new Guid("52340c69-51d7-467b-be1f-885850902380"));

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
                values: new object[] { new Guid("c7108a75-79ac-46e8-8208-e19d6fe8a252"), "FTC-1" });

            migrationBuilder.InsertData(
                table: "Gates",
                columns: new[] { "Id", "Number", "PlatformId" },
                values: new object[,]
                {
                    { new Guid("14338de4-7ed1-4e78-8283-eb1b87060263"), 1, new Guid("c7108a75-79ac-46e8-8208-e19d6fe8a252") },
                    { new Guid("7c846671-27f5-4127-8a28-ac174d34fc86"), 5, new Guid("c7108a75-79ac-46e8-8208-e19d6fe8a252") },
                    { new Guid("ccca44cb-bdb5-4b9c-abc9-87edd970a81d"), 17, new Guid("c7108a75-79ac-46e8-8208-e19d6fe8a252") }
                });
        }
    }
}
