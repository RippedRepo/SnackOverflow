namespace ProjectDemoRecipes.Models
{
    public class Favorite
    {
        public int Id { get; set; }

        // Foreign Keys
        public string UserId { get; set; } = string.Empty; // Assuming ASP.NET Identity
        public User User { get; set; } = null!;
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;
    }
}
