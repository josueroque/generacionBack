using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneracionAPI.Migrations
{
    public partial class InadvertidoValor2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InadvertidoValores_Fecha_Hora_Minuto",
                table: "InadvertidoValores");

            migrationBuilder.DropColumn(
                name: "Minuto",
                table: "InadvertidoValores");

            migrationBuilder.CreateIndex(
                name: "IX_InadvertidoValores_Fecha_Hora",
                table: "InadvertidoValores",
                columns: new[] { "Fecha", "Hora" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InadvertidoValores_Fecha_Hora",
                table: "InadvertidoValores");

            migrationBuilder.AddColumn<int>(
                name: "Minuto",
                table: "InadvertidoValores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InadvertidoValores_Fecha_Hora_Minuto",
                table: "InadvertidoValores",
                columns: new[] { "Fecha", "Hora", "Minuto" },
                unique: true);
        }
    }
}
