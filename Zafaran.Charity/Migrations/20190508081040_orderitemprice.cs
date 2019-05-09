using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Zafaran.Charity.Migrations
{
    public partial class orderitemprice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductPrice",
                table: "OrderItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductSofrehPrice",
                table: "OrderItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ProductSofrehPrice",
                table: "OrderItems");
        }
    }
}
