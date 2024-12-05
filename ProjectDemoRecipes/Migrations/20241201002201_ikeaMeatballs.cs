using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectDemoRecipes.Migrations
{
    /// <inheritdoc />
    public partial class ikeaMeatballs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CookingTime", "Description", "ImageUrl", "IsLactoseFree", "IsVegetarian", "Title" },
                values: new object[] { 3, 50, "Meatballs with mashed potatoes and lingonberry jam", "ikeameatballs.jpg", false, false, "Ikea Meatballs" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
