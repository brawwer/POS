using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace POS.Migrations
{
    public partial class POSDatabase05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AmountUSD",
                table: "DailySalesEntry",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountUSD",
                table: "DailySalesEntry");
        }
    }
}
