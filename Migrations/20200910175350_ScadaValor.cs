using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneracionAPI.Migrations
{
    public partial class ScadaValor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Ruta",
                table: "Archivos",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ScadaValores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valor = table.Column<float>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Hora = table.Column<int>(nullable: false),
                    IdUsuarioGuarda = table.Column<int>(nullable: false),
                    IdUsuarioModifica = table.Column<int>(nullable: false),
                    fechaHoraGuarda = table.Column<DateTime>(nullable: false),
                    fechaHoraModifica = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScadaValores", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScadaValores_Fecha_Hora",
                table: "ScadaValores",
                columns: new[] { "Fecha", "Hora" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScadaValores");

            migrationBuilder.AlterColumn<string>(
                name: "Ruta",
                table: "Archivos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
