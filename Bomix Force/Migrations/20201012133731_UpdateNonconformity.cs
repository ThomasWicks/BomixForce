using Microsoft.EntityFrameworkCore.Migrations;

namespace Bomix_Force.Migrations
{
    public partial class UpdateNonconformity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Nonconformity",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemEnum",
                table: "Nonconformity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nf",
                table: "Nonconformity",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Nonconformity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Nonconformity_CompanyId",
                table: "Nonconformity",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nonconformity_Company_CompanyId",
                table: "Nonconformity",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nonconformity_Company_CompanyId",
                table: "Nonconformity");

            migrationBuilder.DropIndex(
                name: "IX_Nonconformity_CompanyId",
                table: "Nonconformity");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Nonconformity");

            migrationBuilder.DropColumn(
                name: "ItemEnum",
                table: "Nonconformity");

            migrationBuilder.DropColumn(
                name: "Nf",
                table: "Nonconformity");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Nonconformity");
        }
    }
}
