using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneracionAPI.Migrations
{
    public partial class LlaveArchivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fecha",
                table: "Archivos",
                newName: "Fecha");

            migrationBuilder.CreateIndex(
                name: "IX_Archivos_Fecha_SCADA",
                table: "Archivos",
                columns: new[] { "Fecha", "SCADA" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Archivos_Fecha_SCADA",
                table: "Archivos");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Archivos",
                newName: "fecha");
        }
    }
}
