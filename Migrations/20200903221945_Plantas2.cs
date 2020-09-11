using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneracionAPI.Migrations
{
    public partial class Plantas2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plantas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false),
                    Nomenclatura = table.Column<string>(nullable: false),
                    RotulacionSCADA = table.Column<string>(nullable: true),
                    Nodo = table.Column<int>(nullable: false),
                    OrigenId = table.Column<int>(nullable: false),
                    TensionId = table.Column<int>(nullable: false),
                    SubestacionId = table.Column<int>(nullable: false),
                    FuenteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plantas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plantas_Subestaciones_FuenteId",
                        column: x => x.FuenteId,
                        principalTable: "Subestaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Plantas_Origenes_OrigenId",
                        column: x => x.OrigenId,
                        principalTable: "Origenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Plantas_Subestaciones_SubestacionId",
                        column: x => x.SubestacionId,
                        principalTable: "Subestaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Plantas_Tensiones_TensionId",
                        column: x => x.TensionId,
                        principalTable: "Tensiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plantas_FuenteId",
                table: "Plantas",
                column: "FuenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Plantas_OrigenId",
                table: "Plantas",
                column: "OrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_Plantas_SubestacionId",
                table: "Plantas",
                column: "SubestacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Plantas_TensionId",
                table: "Plantas",
                column: "TensionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plantas");
        }
    }
}
