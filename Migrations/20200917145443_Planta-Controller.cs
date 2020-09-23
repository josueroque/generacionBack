using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneracionAPI.Migrations
{
    public partial class PlantaController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plantas_Subestaciones_FuenteId",
                table: "Plantas");

            migrationBuilder.AddForeignKey(
                name: "FK_Plantas_Fuentes_FuenteId",
                table: "Plantas",
                column: "FuenteId",
                principalTable: "Fuentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plantas_Fuentes_FuenteId",
                table: "Plantas");

            migrationBuilder.AddForeignKey(
                name: "FK_Plantas_Subestaciones_FuenteId",
                table: "Plantas",
                column: "FuenteId",
                principalTable: "Subestaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
