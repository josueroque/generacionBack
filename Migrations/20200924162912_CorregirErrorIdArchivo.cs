using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneracionAPI.Migrations
{
    public partial class CorregirErrorIdArchivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScadaValores_Plantas_ArchivoId",
                table: "ScadaValores");

            migrationBuilder.AddForeignKey(
                name: "FK_ScadaValores_Archivos_ArchivoId",
                table: "ScadaValores",
                column: "ArchivoId",
                principalTable: "Archivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScadaValores_Archivos_ArchivoId",
                table: "ScadaValores");

            migrationBuilder.AddForeignKey(
                name: "FK_ScadaValores_Plantas_ArchivoId",
                table: "ScadaValores",
                column: "ArchivoId",
                principalTable: "Plantas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
