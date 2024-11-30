namespace ProjectDemoRecipes.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; } // 1-5 stars

        // Foreign Keys
        public string UserId { get; set; } = string.Empty; // Assuming ASP.NET Identity
        public User User { get; set; } = null!;
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;
    }
}
