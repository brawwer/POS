using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace POS.Migrations
{
    public partial class POSDatabase04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalesRepId",
                table: "DailySalesModel");

            migrationBuilder.AddColumn<string>(
                name: "SalesRep",
                table: "DailySalesModel",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalesRep",
                table: "DailySalesModel");

            migrationBuilder.AddColumn<int>(
                name: "SalesRepId",
                table: "DailySalesModel",
                nullable: false,
                defaultValue: 0);
        }
    }
}
