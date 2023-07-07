using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimeSlots.Migrations
{
    /// <inheritdoc />
    public partial class EntityPlatformFavoriteAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "PlatformFavorites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlatformId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CompanyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MaxTaskCount = table.Column<int>(type: "INTEGER", nullable: false),
                    DaysOfWeekString = table.Column<string>(type: "TEXT", nullable: false),
                    From = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    To = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    TaskTypesString = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformFavorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlatformFavorites_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlatformFavorites_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("4e36a5cf-4c0a-4217-89a8-13588a42223c"), "FTC-1" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name", "PlatformId" },
                values: new object[] { new Guid("f13e9549-9b98-46fb-b696-a990432e2710"), "Company A", new Guid("4e36a5cf-4c0a-4217-89a8-13588a42223c") });

            migrationBuilder.InsertData(
                table: "Gates",
                columns: new[] { "Id", "Number", "PlatformId" },
                values: new object[,]
                {
                    { new Guid("60cb1060-75c8-4dfd-b916-bfb59f8c3dfa"), 2, new Guid("4e36a5cf-4c0a-4217-89a8-13588a42223c") },
                    { new Guid("6fe3698a-df14-4fc3-834c-decaf5b0a6bb"), 18, new Guid("4e36a5cf-4c0a-4217-89a8-13588a42223c") },
                    { new Guid("750c50de-84b1-44b1-955b-a31947624107"), 9, new Guid("4e36a5cf-4c0a-4217-89a8-13588a42223c") }
                });

            migrationBuilder.InsertData(
                table: "GateSchedules",
                columns: new[] { "Id", "CompanyId", "DaysOfWeekString", "From", "GateId", "TaskTypesString", "To" },
                values: new object[,]
                {
                    { new Guid("7ccb98cc-d9ec-43e4-a345-bce3e3b023a6"), new Guid("f13e9549-9b98-46fb-b696-a990432e2710"), "Sunday,Monday,Tuesday", new TimeSpan(0, 12, 0, 0, 0), new Guid("750c50de-84b1-44b1-955b-a31947624107"), "Loading,Unloading,Transfer", new TimeSpan(0, 18, 0, 0, 0) },
                    { new Guid("ddbf481e-7415-4961-afeb-08b7f008f9f1"), null, "Wednesday,Thursday", new TimeSpan(0, 9, 30, 0, 0), new Guid("6fe3698a-df14-4fc3-834c-decaf5b0a6bb"), "Loading", new TimeSpan(0, 15, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "PlatformFavorites",
                columns: new[] { "Id", "CompanyId", "DaysOfWeekString", "From", "MaxTaskCount", "PlatformId", "TaskTypesString", "To" },
                values: new object[] { new Guid("8b005f22-bddb-432f-ba82-a1364d4377f2"), new Guid("f13e9549-9b98-46fb-b696-a990432e2710"), "Monday,Tuesday,Wednesday", new TimeSpan(0, 12, 0, 0, 0), 1, new Guid("4e36a5cf-4c0a-4217-89a8-13588a42223c"), "Unloading", new TimeSpan(0, 18, 0, 0, 0) });

            migrationBuilder.CreateIndex(
                name: "IX_PlatformFavorites_CompanyId",
                table: "PlatformFavorites",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatformFavorites_PlatformId",
                table: "PlatformFavorites",
                column: "PlatformId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlatformFavorites");

            migrationBuilder.DeleteData(
                table: "GateSchedules",
                keyColumn: "Id",
                keyValue: new Guid("7ccb98cc-d9ec-43e4-a345-bce3e3b023a6"));

            migrationBuilder.DeleteData(
                table: "GateSchedules",
                keyColumn: "Id",
                keyValue: new Guid("ddbf481e-7415-4961-afeb-08b7f008f9f1"));

            migrationBuilder.DeleteData(
                table: "Gates",
                keyColumn: "Id",
                keyValue: new Guid("60cb1060-75c8-4dfd-b916-bfb59f8c3dfa"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("f13e9549-9b98-46fb-b696-a990432e2710"));

            migrationBuilder.DeleteData(
                table: "Gates",
                keyColumn: "Id",
                keyValue: new Guid("6fe3698a-df14-4fc3-834c-decaf5b0a6bb"));

            migrationBuilder.DeleteData(
                table: "Gates",
                keyColumn: "Id",
                keyValue: new Guid("750c50de-84b1-44b1-955b-a31947624107"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("4e36a5cf-4c0a-4217-89a8-13588a42223c"));

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
    }
}
