using Microsoft.EntityFrameworkCore.Migrations;

namespace Bomix_Force.Migrations
{
    public partial class DatabaseTeste : Migration
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
                name: "FK_PERSON_COMPANY_CompanyId",
                table: "PERSON");

            migrationBuilder.DropForeignKey(
                name: "FK_PERSON_ORDER_OrderId",
                table: "PERSON");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PERSON",
                table: "PERSON");

            migrationBuilder.DropIndex(
                name: "IX_PERSON_OrderId",
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
                name: "EMAIL",
                table: "PERSON");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "PERSON");

            migrationBuilder.DropColumn(
                name: "TEL",
                table: "PERSON");

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
                name: "EMISSAOGADATE",
                table: "Order",
                newName: "Emissao");

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
                name: "STATUS_ART",
                table: "Item",
                newName: "Status_art");

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
                name: "EMAIL",
                table: "Company",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "CNPJ",
                table: "Company",
                newName: "Cnpj");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Person",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<string>(
                name: "Cargo",
                table: "Person",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Order",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Order",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ComercialId",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Order",
                nullable: true);

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
                name: "Status_art",
                table: "Item",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Item",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Item",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Company",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
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

            migrationBuilder.AddColumn<int>(
                name: "ComercialId",
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
                name: "Comercial",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Setor = table.Column<string>(nullable: true),
                    Cargo = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comercial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comercial_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_UserId",
                table: "Person",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ComercialId",
                table: "Order",
                column: "ComercialId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_PersonId",
                table: "Order",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_ComercialId",
                table: "Company",
                column: "ComercialId");

            migrationBuilder.CreateIndex(
                name: "IX_Comercial_UserId",
                table: "Comercial",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Comercial_ComercialId",
                table: "Company",
                column: "ComercialId",
                principalTable: "Comercial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Order_OrderId",
                table: "Item",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_N_conformity_Order_OrderId",
                table: "N_conformity",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Comercial_ComercialId",
                table: "Order",
                column: "ComercialId",
                principalTable: "Comercial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Company_CompanyId",
                table: "Order",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_AspNetUsers_UserId",
                table: "Person",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Comercial_ComercialId",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Order_OrderId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_N_conformity_Order_OrderId",
                table: "N_conformity");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Comercial_ComercialId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Company_CompanyId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Person_PersonId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Company_CompanyId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_AspNetUsers_UserId",
                table: "Person");

            migrationBuilder.DropTable(
                name: "Comercial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_UserId",
                table: "Person");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ComercialId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_PersonId",
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
                name: "IX_Company_ComercialId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "ComercialId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ComercialId",
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
                newName: "EMISSAOGADATE");

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
                name: "Status_art",
                table: "ITEM",
                newName: "STATUS_ART");

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
                name: "Email",
                table: "COMPANY",
                newName: "EMAIL");

            migrationBuilder.RenameColumn(
                name: "Cnpj",
                table: "COMPANY",
                newName: "CNPJ");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PERSON",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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

            migrationBuilder.AlterColumn<string>(
                name: "CARGO",
                table: "PERSON",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EMAIL",
                table: "PERSON",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "PERSON",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TEL",
                table: "PERSON",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "STATUS",
                table: "ORDER",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "ORDER",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
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
                name: "STATUS_ART",
                table: "ITEM",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "ITEM",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DESCRIPTION",
                table: "ITEM",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NAME",
                table: "COMPANY",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EMAIL",
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

            migrationBuilder.CreateIndex(
                name: "IX_PERSON_OrderId",
                table: "PERSON",
                column: "OrderId");

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
                name: "FK_PERSON_COMPANY_CompanyId",
                table: "PERSON",
                column: "CompanyId",
                principalTable: "COMPANY",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PERSON_ORDER_OrderId",
                table: "PERSON",
                column: "OrderId",
                principalTable: "ORDER",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
