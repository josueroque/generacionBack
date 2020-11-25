using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneracionAPI.Migrations
{
    public partial class CurvaDemandaArchivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArchivoId",
                table: "CurvaDemandaValores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CurvaDemandaValores_ArchivoId",
                table: "CurvaDemandaValores",
                column: "ArchivoId");

            migrationBuilder.CreateIndex(
                name: "IX_CurvaDemandaValores_Fecha_Hora_Minuto",
                table: "CurvaDemandaValores",
                columns: new[] { "Fecha", "Hora", "Minuto" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CurvaDemandaValores_Archivos_ArchivoId",
                table: "CurvaDemandaValores",
                column: "ArchivoId",
                principalTable: "Archivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurvaDemandaValores_Archivos_ArchivoId",
                table: "CurvaDemandaValores");

            migrationBuilder.DropIndex(
                name: "IX_CurvaDemandaValores_ArchivoId",
                table: "CurvaDemandaValores");

            migrationBuilder.DropIndex(
                name: "IX_CurvaDemandaValores_Fecha_Hora_Minuto",
                table: "CurvaDemandaValores");

            migrationBuilder.DropColumn(
                name: "ArchivoId",
                table: "CurvaDemandaValores");
        }
    }
}
