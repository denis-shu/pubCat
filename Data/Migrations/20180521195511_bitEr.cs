using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Bolt.Data.Migrations
{
    public partial class bitEr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategory_Gategory_CategoryId",
                table: "SubCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gategory",
                table: "Gategory");

            migrationBuilder.DropColumn(
                name: "GategoryId",
                table: "SubCategory");

            migrationBuilder.RenameTable(
                name: "Gategory",
                newName: "Category");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "SubCategory",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategory_Category_CategoryId",
                table: "SubCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategory_Category_CategoryId",
                table: "SubCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Gategory");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "SubCategory",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "GategoryId",
                table: "SubCategory",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gategory",
                table: "Gategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategory_Gategory_CategoryId",
                table: "SubCategory",
                column: "CategoryId",
                principalTable: "Gategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
