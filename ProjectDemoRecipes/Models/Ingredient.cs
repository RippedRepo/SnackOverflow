using System.ComponentModel.DataAnnotations;

namespace ProjectDemoRecipes.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Quantity { get; set; } = string.Empty;
        /// 1 tbsp, 2 cups,3 slices

        
        public int RecipeId     { get; set; }
        public Recipe Recipe { get; set; } = null!;

    }
}
