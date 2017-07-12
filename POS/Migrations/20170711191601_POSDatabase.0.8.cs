using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace POS.Migrations
{
    public partial class POSDatabase08 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItem_AddInventoryModel_AddInventoryModelID",
                table: "InventoryItem");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItem_AddInventoryModelID",
                table: "InventoryItem");

            migrationBuilder.DropColumn(
                name: "AddInventoryModelID",
                table: "InventoryItem");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "AddInventoryModel");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AddInventoryModel",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AddedItems",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddInventoryModelId = table.Column<int>(nullable: false),
                    InventoryItemId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddedItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AddedItems_AddInventoryModel_AddInventoryModelId",
                        column: x => x.AddInventoryModelId,
                        principalTable: "AddInventoryModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddedItems_InventoryItem_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "InventoryItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddedItems_AddInventoryModelId",
                table: "AddedItems",
                column: "AddInventoryModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AddedItems_InventoryItemId",
                table: "AddedItems",
                column: "InventoryItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddedItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AddInventoryModel");

            migrationBuilder.AddColumn<int>(
                name: "AddInventoryModelID",
                table: "InventoryItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "AddInventoryModel",
                nullable: false,
                defaultValue: 0);

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
    }
}
