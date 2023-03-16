using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app1.Migrations
{
    /// <inheritdoc />
    public partial class AddLocalUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "localUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Passwrod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_localUser", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateData", "UpdateData" },
                values: new object[] { new DateTime(2023, 3, 13, 9, 30, 32, 966, DateTimeKind.Local).AddTicks(7800), new DateTime(2023, 3, 13, 9, 30, 32, 966, DateTimeKind.Local).AddTicks(7839) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreateData", "UpdateData" },
                values: new object[] { new DateTime(2023, 3, 13, 9, 30, 32, 966, DateTimeKind.Local).AddTicks(7843), new DateTime(2023, 3, 13, 9, 30, 32, 966, DateTimeKind.Local).AddTicks(7845) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreateData", "UpdateData" },
                values: new object[] { new DateTime(2023, 3, 13, 9, 30, 32, 966, DateTimeKind.Local).AddTicks(7847), new DateTime(2023, 3, 13, 9, 30, 32, 966, DateTimeKind.Local).AddTicks(7849) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "localUser");

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
        }
    }
}
