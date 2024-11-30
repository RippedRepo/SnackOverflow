namespace ProjectDemoRecipes.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CookingTime { get; set; } // In minutes
        public bool IsVegetarian { get; set; }
        public bool IsLactoseFree { get; set; }
        public string ImageUrl { get; set; } = string.Empty; // URL for recipe image

        
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public List<Review> Reviews { get; set; }
    }
}
