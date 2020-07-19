using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bomix_Force.Migrations
{
    public partial class Inita : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PERMISSION",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_USER = table.Column<int>(nullable: true),
                    CLAIMTYPE = table.Column<string>(maxLength: 100, nullable: true),
                    CLAIMVALUE = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERMISSION", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PROFILE",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(maxLength: 50, nullable: false),
                    ACTIVE = table.Column<string>(maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROFILE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ACCESS",
                columns: table => new
                {
                    ID_PROFILE = table.Column<int>(nullable: false),
                    ID_PERMISSION = table.Column<int>(nullable: false),
                    ID_USER = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCESS", x => new { x.ID_PROFILE, x.ID_PERMISSION });
                    table.ForeignKey(
                        name: "FK_ACCESS_PERMISSION_ID_PERMISSION",
                        column: x => x.ID_PERMISSION,
                        principalTable: "PERMISSION",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ACCESS_PROFILE_ID_PROFILE",
                        column: x => x.ID_PROFILE,
                        principalTable: "PROFILE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_PROFILE = table.Column<int>(nullable: false),
                    CPF = table.Column<string>(maxLength: 11, nullable: false),
                    NAME = table.Column<string>(maxLength: 50, nullable: false),
                    EMAIL = table.Column<string>(maxLength: 50, nullable: false),
                    ACTIVE = table.Column<string>(type: "CHAR", maxLength: 1, nullable: false),
                    USERNAME = table.Column<string>(maxLength: 50, nullable: false),
                    ID_ESTABLISHMENT = table.Column<int>(nullable: false),
                    EMAILCONFIRMED = table.Column<bool>(nullable: false),
                    PASSWORDHASH = table.Column<string>(nullable: false),
                    SECURITYSTAMP = table.Column<string>(nullable: true),
                    PHONENUMBER = table.Column<string>(maxLength: 50, nullable: true),
                    PHONENUMBERCONFIRMED = table.Column<bool>(nullable: false),
                    TWOFACTORENABLED = table.Column<bool>(nullable: false),
                    LOCKOUTENDDATEUTC = table.Column<DateTime>(nullable: true),
                    LOCKOUTENABLED = table.Column<bool>(nullable: false),
                    RECIEVENOTIFICATION = table.Column<bool>(nullable: false),
                    ACCESSFAILEDCOUNT = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_PROFILE_ID_PROFILE",
                        column: x => x.ID_PROFILE,
                        principalTable: "PROFILE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USERLOGIN",
                columns: table => new
                {
                    ID_USER = table.Column<int>(nullable: false),
                    LOGINPROVIDER = table.Column<string>(maxLength: 128, nullable: false),
                    PROVIDERKEY = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERLOGIN", x => new { x.ID_USER, x.LOGINPROVIDER, x.PROVIDERKEY });
                    table.ForeignKey(
                        name: "FK_USERLOGIN_USER_ID_USER",
                        column: x => x.ID_USER,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ACCESS_ID_PERMISSION",
                table: "ACCESS",
                column: "ID_PERMISSION");

            migrationBuilder.CreateIndex(
                name: "IX_USER_ID_PROFILE",
                table: "USER",
                column: "ID_PROFILE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACCESS");

            migrationBuilder.DropTable(
                name: "USERLOGIN");

            migrationBuilder.DropTable(
                name: "PERMISSION");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "PROFILE");
        }
    }
}
