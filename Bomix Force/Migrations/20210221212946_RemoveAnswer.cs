using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bomix_Force.Migrations
{
    public partial class RemoveAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Nonconformity");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<byte[]>(
                name: "Answer",
                table: "Nonconformity",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
