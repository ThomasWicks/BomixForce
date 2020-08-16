using Microsoft.EntityFrameworkCore.Migrations;

namespace Bomix_Force.Migrations
{
    public partial class cpfremove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "PERSON");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CPF",
                table: "PERSON",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
