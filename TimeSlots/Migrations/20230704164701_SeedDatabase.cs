using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TimeSlots.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
