using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneracionAPI.Migrations
{
    public partial class ScadaValorPlantaIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ScadaValores_Fecha_Hora",
                table: "ScadaValores");

            migrationBuilder.CreateIndex(
                name: "IX_ScadaValores_Fecha_Hora_PlantaId",
                table: "ScadaValores",
                columns: new[] { "Fecha", "Hora", "PlantaId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ScadaValores_Fecha_Hora_PlantaId",
                table: "ScadaValores");

            migrationBuilder.CreateIndex(
                name: "IX_ScadaValores_Fecha_Hora",
                table: "ScadaValores",
                columns: new[] { "Fecha", "Hora" },
                unique: true);
        }
    }
}
