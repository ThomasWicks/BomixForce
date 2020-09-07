using Microsoft.EntityFrameworkCore.Migrations;

namespace Bomix_Force.Migrations
{
    public partial class User_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ITEM_ORDER_OrderId",
                table: "ITEM");

            migrationBuilder.DropForeignKey(
                name: "FK_N_CONFORMITY_ORDER_OrderId",
                table: "N_CONFORMITY");

            migrationBuilder.DropForeignKey(
                name: "FK_ORDER_COMPANY_CompanyId",
                table: "ORDER");

            migrationBuilder.DropForeignKey(
                name: "FK_ORDER_PERSON_PersonId",
                table: "ORDER");

            migrationBuilder.DropForeignKey(
                name: "FK_PERSON_COMPANY_CompanyId",
                table: "PERSON");

            migrationBuilder.DropTable(
                name: "UserViewEdit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PERSON",
                table: "PERSON");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ORDER",
                table: "ORDER");

            migrationBuilder.DropPrimaryKey(
                name: "PK_N_CONFORMITY",
                table: "N_CONFORMITY");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ITEM",
                table: "ITEM");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DOCUMENT",
                table: "DOCUMENT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_COMPANY",
                table: "COMPANY");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PERSON");

            migrationBuilder.DropColumn(
                name: "STATUS_ART",
                table: "ITEM");

            migrationBuilder.DropColumn(
                name: "EMAIL",
                table: "COMPANY");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "COMPANY");

            migrationBuilder.RenameTable(
                name: "PERSON",
                newName: "Person");

            migrationBuilder.RenameTable(
                name: "ORDER",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "N_CONFORMITY",
                newName: "N_conformity");

            migrationBuilder.RenameTable(
                name: "ITEM",
                newName: "Item");

            migrationBuilder.RenameTable(
                name: "DOCUMENT",
                newName: "Document");

            migrationBuilder.RenameTable(
                name: "COMPANY",
                newName: "Company");

            migrationBuilder.RenameColumn(
                name: "SETOR",
                table: "Person",
                newName: "Setor");

            migrationBuilder.RenameColumn(
                name: "NAME",
                table: "Person",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CARGO",
                table: "Person",
                newName: "Cargo");

            migrationBuilder.RenameIndex(
                name: "IX_PERSON_CompanyId",
                table: "Person",
                newName: "IX_Person_CompanyId");

            migrationBuilder.RenameColumn(
                name: "STATUS",
                table: "Order",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "NUMBER",
                table: "Order",
                newName: "NumeroPedido");

            migrationBuilder.RenameColumn(
                name: "ENTREGADATE",
                table: "Order",
                newName: "Entrega");

            migrationBuilder.RenameColumn(
                name: "EMISSAODATE",
                table: "Order",
                newName: "Emissao");

            migrationBuilder.RenameIndex(
                name: "IX_ORDER_PersonId",
                table: "Order",
                newName: "IX_Order_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_ORDER_CompanyId",
                table: "Order",
                newName: "IX_Order_CompanyId");

            migrationBuilder.RenameColumn(
                name: "LOT",
                table: "N_conformity",
                newName: "Lot");

            migrationBuilder.RenameColumn(
                name: "DESCRIPTION",
                table: "N_conformity",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_N_CONFORMITY_OrderId",
                table: "N_conformity",
                newName: "IX_N_conformity_OrderId");

            migrationBuilder.RenameColumn(
                name: "DESCRIPTION",
                table: "Item",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_ITEM_OrderId",
                table: "Item",
                newName: "IX_Item_OrderId");

            migrationBuilder.RenameColumn(
                name: "NAME",
                table: "Company",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CNPJ",
                table: "Company",
                newName: "Cnpj");

            migrationBuilder.AlterColumn<string>(
                name: "Setor",
                table: "Person",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Person",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Person",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cargo",
                table: "Person",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Person",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Order",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Order",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "N_conformity",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Lot",
                table: "N_conformity",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "N_conformity",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Item",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Document",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Company",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "Company",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Company",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Person",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_N_conformity",
                table: "N_conformity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                table: "Item",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Document",
                table: "Document",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Company",
                table: "Company",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(nullable: true),
                    Idtotvs = table.Column<string>(nullable: true),
                    IdentityUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_IdentityUserId",
                table: "Person",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_EmployeeId",
                table: "Order",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_IdentityUserId",
                table: "Company",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_IdentityUserId",
                table: "Employee",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_AspNetUsers_IdentityUserId",
                table: "Company",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Order_OrderId",
                table: "Item",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_N_conformity_Order_OrderId",
                table: "N_conformity",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Company_CompanyId",
                table: "Order",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Employee_EmployeeId",
                table: "Order",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Person_PersonId",
                table: "Order",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Company_CompanyId",
                table: "Person",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_AspNetUsers_IdentityUserId",
                table: "Person",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_AspNetUsers_IdentityUserId",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Order_OrderId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_N_conformity_Order_OrderId",
                table: "N_conformity");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Company_CompanyId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Employee_EmployeeId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Person_PersonId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Company_CompanyId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_AspNetUsers_IdentityUserId",
                table: "Person");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_IdentityUserId",
                table: "Person");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_EmployeeId",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_N_conformity",
                table: "N_conformity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                table: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Document",
                table: "Document");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Company",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_IdentityUserId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Company");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "PERSON");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "ORDER");

            migrationBuilder.RenameTable(
                name: "N_conformity",
                newName: "N_CONFORMITY");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "ITEM");

            migrationBuilder.RenameTable(
                name: "Document",
                newName: "DOCUMENT");

            migrationBuilder.RenameTable(
                name: "Company",
                newName: "COMPANY");

            migrationBuilder.RenameColumn(
                name: "Setor",
                table: "PERSON",
                newName: "SETOR");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PERSON",
                newName: "NAME");

            migrationBuilder.RenameColumn(
                name: "Cargo",
                table: "PERSON",
                newName: "CARGO");

            migrationBuilder.RenameIndex(
                name: "IX_Person_CompanyId",
                table: "PERSON",
                newName: "IX_PERSON_CompanyId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "ORDER",
                newName: "STATUS");

            migrationBuilder.RenameColumn(
                name: "NumeroPedido",
                table: "ORDER",
                newName: "NUMBER");

            migrationBuilder.RenameColumn(
                name: "Entrega",
                table: "ORDER",
                newName: "ENTREGADATE");

            migrationBuilder.RenameColumn(
                name: "Emissao",
                table: "ORDER",
                newName: "EMISSAODATE");

            migrationBuilder.RenameIndex(
                name: "IX_Order_PersonId",
                table: "ORDER",
                newName: "IX_ORDER_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CompanyId",
                table: "ORDER",
                newName: "IX_ORDER_CompanyId");

            migrationBuilder.RenameColumn(
                name: "Lot",
                table: "N_CONFORMITY",
                newName: "LOT");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "N_CONFORMITY",
                newName: "DESCRIPTION");

            migrationBuilder.RenameIndex(
                name: "IX_N_conformity_OrderId",
                table: "N_CONFORMITY",
                newName: "IX_N_CONFORMITY_OrderId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ITEM",
                newName: "DESCRIPTION");

            migrationBuilder.RenameIndex(
                name: "IX_Item_OrderId",
                table: "ITEM",
                newName: "IX_ITEM_OrderId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "COMPANY",
                newName: "NAME");

            migrationBuilder.RenameColumn(
                name: "Cnpj",
                table: "COMPANY",
                newName: "CNPJ");

            migrationBuilder.AlterColumn<string>(
                name: "SETOR",
                table: "PERSON",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NAME",
                table: "PERSON",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "PERSON",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "CARGO",
                table: "PERSON",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PERSON",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "STATUS",
                table: "ORDER",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "N_CONFORMITY",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LOT",
                table: "N_CONFORMITY",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DESCRIPTION",
                table: "N_CONFORMITY",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DESCRIPTION",
                table: "ITEM",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STATUS_ART",
                table: "ITEM",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "NAME",
                table: "COMPANY",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CNPJ",
                table: "COMPANY",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EMAIL",
                table: "COMPANY",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "COMPANY",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PERSON",
                table: "PERSON",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ORDER",
                table: "ORDER",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_N_CONFORMITY",
                table: "N_CONFORMITY",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ITEM",
                table: "ITEM",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DOCUMENT",
                table: "DOCUMENT",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_COMPANY",
                table: "COMPANY",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserViewEdit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Setor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tel = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserViewEdit", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ITEM_ORDER_OrderId",
                table: "ITEM",
                column: "OrderId",
                principalTable: "ORDER",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_N_CONFORMITY_ORDER_OrderId",
                table: "N_CONFORMITY",
                column: "OrderId",
                principalTable: "ORDER",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ORDER_COMPANY_CompanyId",
                table: "ORDER",
                column: "CompanyId",
                principalTable: "COMPANY",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ORDER_PERSON_PersonId",
                table: "ORDER",
                column: "PersonId",
                principalTable: "PERSON",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PERSON_COMPANY_CompanyId",
                table: "PERSON",
                column: "CompanyId",
                principalTable: "COMPANY",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
