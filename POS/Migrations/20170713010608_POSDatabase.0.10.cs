using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace POS.Migrations
{
    public partial class POSDatabase010 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AddInventoryModel",
                newName: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "AddInventoryModel",
                newName: "UserId");
        }
    }
}
