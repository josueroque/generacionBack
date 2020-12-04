using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneracionAPI.Migrations
{
    public partial class InadvertidoValor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InadvertidoValores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Hora = table.Column<int>(nullable: false),
                    Minuto = table.Column<int>(nullable: false),
                    AMM = table.Column<float>(nullable: true),
                    UT = table.Column<float>(nullable: true),
                    ENATREL = table.Column<float>(nullable: true),
                    ArchivoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InadvertidoValores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InadvertidoValores_Archivos_ArchivoId",
                        column: x => x.ArchivoId,
                        principalTable: "Archivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InadvertidoValores_ArchivoId",
                table: "InadvertidoValores",
                column: "ArchivoId");

            migrationBuilder.CreateIndex(
                name: "IX_InadvertidoValores_Fecha_Hora_Minuto",
                table: "InadvertidoValores",
                columns: new[] { "Fecha", "Hora", "Minuto" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InadvertidoValores");
        }
    }
}
