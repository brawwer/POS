using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace POS.Migrations
{
    public partial class POSDatabase09 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddedItems");

            migrationBuilder.CreateTable(
                name: "AddedItem",
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
                    table.PrimaryKey("PK_AddedItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AddedItem_AddInventoryModel_AddInventoryModelId",
                        column: x => x.AddInventoryModelId,
                        principalTable: "AddInventoryModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddedItem_InventoryItem_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "InventoryItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddedItem_AddInventoryModelId",
                table: "AddedItem",
                column: "AddInventoryModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AddedItem_InventoryItemId",
                table: "AddedItem",
                column: "InventoryItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddedItem");

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
    }
}
