using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace POS.Migrations
{
    public partial class POSDatabase06 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ItemPriceCOP",
                table: "DailySalesEntry",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ItemPriceUSD",
                table: "DailySalesEntry",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemPriceCOP",
                table: "DailySalesEntry");

            migrationBuilder.DropColumn(
                name: "ItemPriceUSD",
                table: "DailySalesEntry");
        }
    }
}
