using ProjectDemoRecipes.Models;

public interface IRecipeRepository
{
    Task<IEnumerable<Recipe>> GetRecipesAsync(string? searchQuery, bool? isVegetarian, bool? isLactoseFree);
    Task<Recipe?> GetRecipeByIdAsync(int id);
    Task AddRecipeAsync(Recipe recipe);
    Task UpdateRecipeAsync(Recipe recipe);
    Task DeleteRecipeAsync(int id);
    Task<bool> RecipeExistsAsync(int id);
}
