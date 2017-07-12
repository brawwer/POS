using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace POS.Migrations
{
    public partial class POSDatabase02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ref",
                table: "InventoryItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ref",
                table: "InventoryItem");
        }
    }
}
