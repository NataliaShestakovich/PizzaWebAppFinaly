using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaWebAppAuthentication.Migrations
{
    /// <inheritdoc />
    public partial class WithoutCombination : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Composition",
                table: "Pizzas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Composition",
                table: "Pizzas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
