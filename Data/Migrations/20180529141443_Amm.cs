using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Bolt.Data.Migrations
{
    public partial class Amm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinimumAmmout",
                table: "Coupons",
                newName: "MinimumAmout");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinimumAmout",
                table: "Coupons",
                newName: "MinimumAmmout");
        }
    }
}
