using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextoCore.Migrations
{
    public partial class quitaridusuarioentablapais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Paises");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Paises",
                nullable: false,
                defaultValue: 0);
        }
    }
}
