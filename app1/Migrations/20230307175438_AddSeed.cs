using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace app1.Migrations
{
    /// <inheritdoc />
    public partial class AddSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "CreateData", "Description", "ImageUrl", "Name", "Rate", "UpdateData", "sqft" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 3, 7, 17, 54, 38, 43, DateTimeKind.Local).AddTicks(1513), "Experience luxury living in this beautiful villa, complete with private pool and stunning views of the ocean.", "https://example.com/luxury-villa.jpg", "Luxury Villa", 500.0, new DateTime(2023, 3, 7, 17, 54, 38, 43, DateTimeKind.Local).AddTicks(1561), 3000 },
                    { 2, new DateTime(2023, 3, 7, 17, 54, 38, 43, DateTimeKind.Local).AddTicks(1565), "Relax and unwind in this beachfront villa, just steps away from the pristine white sands of the beach.", "https://example.com/beachfront-villa.jpg", "Beachfront Villa", 350.0, new DateTime(2023, 3, 7, 17, 54, 38, 43, DateTimeKind.Local).AddTicks(1567), 2000 },
                    { 3, new DateTime(2023, 3, 7, 17, 54, 38, 43, DateTimeKind.Local).AddTicks(1570), "Escape to this tropical paradise villa, surrounded by lush greenery and a serene waterfall.", "https://example.com/tropical-paradise-villa.jpg", "Tropical Paradise Villa", 450.0, new DateTime(2023, 3, 7, 17, 54, 38, 43, DateTimeKind.Local).AddTicks(1572), 2500 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
