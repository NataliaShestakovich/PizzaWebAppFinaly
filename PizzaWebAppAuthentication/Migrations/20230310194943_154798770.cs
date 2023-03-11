using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaWebAppAuthentication.Migrations
{
    /// <inheritdoc />
    public partial class _154798770 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CartItems_PizzaId",
                table: "CartItems",
                column: "PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Pizzas_PizzaId",
                table: "CartItems",
                column: "PizzaId",
                principalTable: "Pizzas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Pizzas_PizzaId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_PizzaId",
                table: "CartItems");
        }
    }
}
