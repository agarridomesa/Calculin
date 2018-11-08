using Microsoft.EntityFrameworkCore.Migrations;

namespace ContextoCore.Migrations
{
    public partial class Conversionintstring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdDestinosiglas",
                table: "Conversiones");

            migrationBuilder.DropColumn(
                name: "IdOrigensiglas",
                table: "Conversiones");

            migrationBuilder.AddColumn<string>(
                name: "Destinosiglas",
                table: "Conversiones",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Origensiglas",
                table: "Conversiones",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destinosiglas",
                table: "Conversiones");

            migrationBuilder.DropColumn(
                name: "Origensiglas",
                table: "Conversiones");

            migrationBuilder.AddColumn<int>(
                name: "IdDestinosiglas",
                table: "Conversiones",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdOrigensiglas",
                table: "Conversiones",
                nullable: false,
                defaultValue: 0);
        }
    }
}
