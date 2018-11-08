using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextoCore.Migrations
{
    public partial class siglasdemonedasenelhistorial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SMD",
                table: "Historiales",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SMO",
                table: "Historiales",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SMD",
                table: "Historiales");

            migrationBuilder.DropColumn(
                name: "SMO",
                table: "Historiales");
        }
    }
}
