using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app1.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyInVillaNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaID",
                table: "VillaNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateData", "UpdateData" },
                values: new object[] { new DateTime(2023, 3, 11, 17, 0, 22, 614, DateTimeKind.Local).AddTicks(7309), new DateTime(2023, 3, 11, 17, 0, 22, 614, DateTimeKind.Local).AddTicks(7355) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateData", "UpdateData" },
                values: new object[] { new DateTime(2023, 3, 11, 17, 0, 22, 614, DateTimeKind.Local).AddTicks(7359), new DateTime(2023, 3, 11, 17, 0, 22, 614, DateTimeKind.Local).AddTicks(7360) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateData", "UpdateData" },
                values: new object[] { new DateTime(2023, 3, 11, 17, 0, 22, 614, DateTimeKind.Local).AddTicks(7363), new DateTime(2023, 3, 11, 17, 0, 22, 614, DateTimeKind.Local).AddTicks(7365) });

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumbers_VillaID",
                table: "VillaNumbers",
                column: "VillaID");

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumbers_Villas_VillaID",
                table: "VillaNumbers",
                column: "VillaID",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumbers_Villas_VillaID",
                table: "VillaNumbers");

            migrationBuilder.DropIndex(
                name: "IX_VillaNumbers_VillaID",
                table: "VillaNumbers");

            migrationBuilder.DropColumn(
                name: "VillaID",
                table: "VillaNumbers");

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
    }
}
