using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneracionAPI.Migrations
{
    public partial class DatosComerciales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUsuarioGuarda",
                table: "ScadaValores");

            migrationBuilder.DropColumn(
                name: "IdUsuarioModifica",
                table: "ScadaValores");

            migrationBuilder.DropColumn(
                name: "fechaHoraGuarda",
                table: "ScadaValores");

            migrationBuilder.DropColumn(
                name: "fechaHoraModifica",
                table: "ScadaValores");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdUsuarioGuarda",
                table: "ScadaValores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdUsuarioModifica",
                table: "ScadaValores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaHoraGuarda",
                table: "ScadaValores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaHoraModifica",
                table: "ScadaValores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
