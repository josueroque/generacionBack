using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneracionAPI.Migrations
{
    public partial class DatosComerciales2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComercialDatos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Entregado = table.Column<float>(nullable: false),
                    Recibido = table.Column<float>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Hora = table.Column<int>(nullable: false),
                    PlantaId = table.Column<int>(nullable: false),
                    ArchivoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComercialDatos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComercialDatos_Archivos_ArchivoId",
                        column: x => x.ArchivoId,
                        principalTable: "Archivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComercialDatos_Plantas_PlantaId",
                        column: x => x.PlantaId,
                        principalTable: "Plantas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComercialDatos_ArchivoId",
                table: "ComercialDatos",
                column: "ArchivoId");

            migrationBuilder.CreateIndex(
                name: "IX_ComercialDatos_PlantaId",
                table: "ComercialDatos",
                column: "PlantaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComercialDatos_Fecha_Hora_PlantaId",
                table: "ComercialDatos",
                columns: new[] { "Fecha", "Hora", "PlantaId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComercialDatos");
        }
    }
}
