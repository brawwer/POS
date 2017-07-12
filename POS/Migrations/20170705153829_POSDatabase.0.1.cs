using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace POS.Migrations
{
    public partial class POSDatabase01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailySalesModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    SalesRepId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailySalesModel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItem",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cost = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PriceCOP = table.Column<double>(nullable: false),
                    PriceUSD = table.Column<double>(nullable: false),
                    ReorderQty = table.Column<int>(nullable: false),
                    StockQty = table.Column<int>(nullable: false),
                    VendorAddress = table.Column<string>(nullable: true),
                    VendorName = table.Column<string>(nullable: true),
                    VendorPhone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItem", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ItemsSoldDaily",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DailySalesId = table.Column<int>(nullable: false),
                    DailySalesModelID = table.Column<int>(nullable: true),
                    SalesRepId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsSoldDaily", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ItemsSoldDaily_DailySalesModel_DailySalesModelID",
                        column: x => x.DailySalesModelID,
                        principalTable: "DailySalesModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DailySalesEntry",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmountCOP = table.Column<double>(nullable: false),
                    DailySalesId = table.Column<int>(nullable: false),
                    InventoryItemId = table.Column<int>(nullable: false),
                    PaymentMethodId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailySalesEntry", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DailySalesEntry_DailySalesModel_DailySalesId",
                        column: x => x.DailySalesId,
                        principalTable: "DailySalesModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailySalesEntry_InventoryItem_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "InventoryItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailySalesEntry_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailySalesEntry_DailySalesId",
                table: "DailySalesEntry",
                column: "DailySalesId");

            migrationBuilder.CreateIndex(
                name: "IX_DailySalesEntry_InventoryItemId",
                table: "DailySalesEntry",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DailySalesEntry_PaymentMethodId",
                table: "DailySalesEntry",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsSoldDaily_DailySalesModelID",
                table: "ItemsSoldDaily",
                column: "DailySalesModelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailySalesEntry");

            migrationBuilder.DropTable(
                name: "ItemsSoldDaily");

            migrationBuilder.DropTable(
                name: "InventoryItem");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "DailySalesModel");
        }
    }
}
