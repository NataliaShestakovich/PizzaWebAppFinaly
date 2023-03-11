using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaWebAppAuthentication.Migrations
{
    /// <inheritdoc />
    public partial class _154798779 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Pizzas_PizzaId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PizzaId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PizzaId",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PizzaId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PizzaId",
                table: "Orders",
                column: "PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Pizzas_PizzaId",
                table: "Orders",
                column: "PizzaId",
                principalTable: "Pizzas",
                principalColumn: "Id");
        }
    }
}
