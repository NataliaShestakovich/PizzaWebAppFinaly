using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaWebAppAuthentication.Migrations
{
    /// <inheritdoc />
    public partial class WithoutDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Pizzas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Pizzas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
