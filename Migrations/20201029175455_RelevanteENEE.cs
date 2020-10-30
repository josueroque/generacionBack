using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneracionAPI.Migrations
{
    public partial class RelevanteENEE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RelevanteENEE",
                table: "Plantas",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelevanteENEE",
                table: "Plantas");
        }
    }
}
