using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace POS.Migrations
{
    public partial class POSDatabase07 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddInventoryModelID",
                table: "InventoryItem",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AddInventoryModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddInventoryModel", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_AddInventoryModelID",
                table: "InventoryItem",
                column: "AddInventoryModelID");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItem_AddInventoryModel_AddInventoryModelID",
                table: "InventoryItem",
                column: "AddInventoryModelID",
                principalTable: "AddInventoryModel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItem_AddInventoryModel_AddInventoryModelID",
                table: "InventoryItem");

            migrationBuilder.DropTable(
                name: "AddInventoryModel");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItem_AddInventoryModelID",
                table: "InventoryItem");

            migrationBuilder.DropColumn(
                name: "AddInventoryModelID",
                table: "InventoryItem");
        }
    }
}
