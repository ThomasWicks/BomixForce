using Microsoft.EntityFrameworkCore.Migrations;

namespace Bomix_Force.Migrations
{
    public partial class CompanyUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "COMPANY",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "COMPANY");
        }
    }
}
