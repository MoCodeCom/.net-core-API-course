using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app1.Migrations
{
    /// <inheritdoc />
    public partial class AddVillaNumberToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VillaNumbers",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillaNumbers", x => x.VillaNo);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateData", "UpdateData" },
                values: new object[] { new DateTime(2023, 3, 11, 5, 51, 48, 183, DateTimeKind.Local).AddTicks(924), new DateTime(2023, 3, 11, 5, 51, 48, 183, DateTimeKind.Local).AddTicks(974) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateData", "UpdateData" },
                values: new object[] { new DateTime(2023, 3, 11, 5, 51, 48, 183, DateTimeKind.Local).AddTicks(978), new DateTime(2023, 3, 11, 5, 51, 48, 183, DateTimeKind.Local).AddTicks(980) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateData", "UpdateData" },
                values: new object[] { new DateTime(2023, 3, 11, 5, 51, 48, 183, DateTimeKind.Local).AddTicks(983), new DateTime(2023, 3, 11, 5, 51, 48, 183, DateTimeKind.Local).AddTicks(986) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VillaNumbers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateData", "UpdateData" },
                values: new object[] { new DateTime(2023, 3, 11, 5, 48, 13, 988, DateTimeKind.Local).AddTicks(179), new DateTime(2023, 3, 11, 5, 48, 13, 988, DateTimeKind.Local).AddTicks(253) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateData", "UpdateData" },
                values: new object[] { new DateTime(2023, 3, 11, 5, 48, 13, 988, DateTimeKind.Local).AddTicks(262), new DateTime(2023, 3, 11, 5, 48, 13, 988, DateTimeKind.Local).AddTicks(266) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateData", "UpdateData" },
                values: new object[] { new DateTime(2023, 3, 11, 5, 48, 13, 988, DateTimeKind.Local).AddTicks(272), new DateTime(2023, 3, 11, 5, 48, 13, 988, DateTimeKind.Local).AddTicks(275) });
        }
    }
}
