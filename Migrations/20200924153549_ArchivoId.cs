using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneracionAPI.Migrations
{
    public partial class ArchivoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArchivoId",
                table: "ScadaValores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ScadaValores_ArchivoId",
                table: "ScadaValores",
                column: "ArchivoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScadaValores_Plantas_ArchivoId",
                table: "ScadaValores",
                column: "ArchivoId",
                principalTable: "Plantas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScadaValores_Plantas_ArchivoId",
                table: "ScadaValores");

            migrationBuilder.DropIndex(
                name: "IX_ScadaValores_ArchivoId",
                table: "ScadaValores");

            migrationBuilder.DropColumn(
                name: "ArchivoId",
                table: "ScadaValores");
        }
    }
}
