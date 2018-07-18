using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DrinkAndGo.Migrations
{
    public partial class Cart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "ShoppingCartItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DrinkId",
                table: "ShoppingCartItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShoppingCartId",
                table: "ShoppingCartItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_DrinkId",
                table: "ShoppingCartItems",
                column: "DrinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Drinks_DrinkId",
                table: "ShoppingCartItems",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "DrinkId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Drinks_DrinkId",
                table: "ShoppingCartItems");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartItems_DrinkId",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "DrinkId",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "ShoppingCartItems");
        }
    }
}
