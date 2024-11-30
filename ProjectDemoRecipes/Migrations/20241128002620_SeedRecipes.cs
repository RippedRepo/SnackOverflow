using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectDemoRecipes.Migrations
{
    /// <inheritdoc />
    public partial class SeedRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CookingTime", "Description", "ImageUrl", "IsLactoseFree", "IsVegetarian", "Title" },
                values: new object[,]
                {
                    { 1, 30, "A classic Italian pasta dish.", "spaghetti.jpg", true, false, "Spaghetti Bolognese" },
                    { 2, 45, "Delicious and cheesy vegetarian lasagna.", "lasagna.jpg", false, true, "Vegetarian Lasagna" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
