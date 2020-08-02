using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bomix_Force.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COMPANY",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(nullable: false),
                    EMAIL = table.Column<string>(nullable: false),
                    CNPJ = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPANY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DOCUMENT",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOCUMENT", x => x.Id);
                });

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
                name: "ORDER",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NUMBER = table.Column<int>(nullable: false),
                    DATE = table.Column<DateTime>(nullable: false),
                    STATUS = table.Column<string>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ORDER_COMPANY_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "COMPANY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EMAILCONFIRMED = table.Column<bool>(nullable: false),
                    PASSWORDHASH = table.Column<string>(nullable: false),
                    SECURITYSTAMP = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PHONENUMBER = table.Column<string>(maxLength: 50, nullable: true),
                    PHONENUMBERCONFIRMED = table.Column<bool>(nullable: false),
                    TWOFACTORENABLED = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LOCKOUTENABLED = table.Column<bool>(nullable: false),
                    ACCESSFAILEDCOUNT = table.Column<int>(type: "int", nullable: false),
                    ID_PROFILE = table.Column<int>(type: "int", nullable: false),
                    ACTIVE = table.Column<string>(type: "CHAR", maxLength: 1, nullable: false),
                    LOCKOUTENDDATEUTC = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_PROFILE_ID_PROFILE",
                        column: x => x.ID_PROFILE,
                        principalTable: "PROFILE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ITEM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STATUS_ART = table.Column<string>(nullable: false),
                    DESCRIPTION = table.Column<string>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITEM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ITEM_ORDER_OrderId",
                        column: x => x.OrderId,
                        principalTable: "ORDER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "N_CONFORMITY",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LOT = table.Column<string>(nullable: false),
                    DESCRIPTION = table.Column<string>(nullable: false),
                    Id_Order = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_N_CONFORMITY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_N_CONFORMITY_ORDER_OrderId",
                        column: x => x.OrderId,
                        principalTable: "ORDER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PERSON",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(nullable: false),
                    EMAIL = table.Column<string>(nullable: false),
                    CPF = table.Column<int>(nullable: false),
                    TEL = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSON", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PERSON_COMPANY_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "COMPANY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PERSON_ORDER_OrderId",
                        column: x => x.OrderId,
                        principalTable: "ORDER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PERSON_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USERLOGIN",
                columns: table => new
                {
                    ID_USER = table.Column<int>(nullable: false),
                    LOGINPROVIDER = table.Column<string>(maxLength: 128, nullable: false),
                    PROVIDERKEY = table.Column<string>(maxLength: 128, nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERLOGIN", x => new { x.ID_USER, x.LOGINPROVIDER, x.PROVIDERKEY });
                    table.ForeignKey(
                        name: "FK_USERLOGIN_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ACCESS_ID_PERMISSION",
                table: "ACCESS",
                column: "ID_PERMISSION");

            migrationBuilder.CreateIndex(
                name: "IX_ITEM_OrderId",
                table: "ITEM",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_N_CONFORMITY_OrderId",
                table: "N_CONFORMITY",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_CompanyId",
                table: "ORDER",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PERSON_CompanyId",
                table: "PERSON",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PERSON_OrderId",
                table: "PERSON",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PERSON_UserId",
                table: "PERSON",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ID_PROFILE",
                table: "User",
                column: "ID_PROFILE");

            migrationBuilder.CreateIndex(
                name: "IX_USERLOGIN_UserId",
                table: "USERLOGIN",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACCESS");

            migrationBuilder.DropTable(
                name: "DOCUMENT");

            migrationBuilder.DropTable(
                name: "ITEM");

            migrationBuilder.DropTable(
                name: "N_CONFORMITY");

            migrationBuilder.DropTable(
                name: "PERSON");

            migrationBuilder.DropTable(
                name: "USERLOGIN");

            migrationBuilder.DropTable(
                name: "PERMISSION");

            migrationBuilder.DropTable(
                name: "ORDER");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "COMPANY");

            migrationBuilder.DropTable(
                name: "PROFILE");
        }
    }
}
