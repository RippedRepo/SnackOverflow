using ProjectDemoRecipes.Data;
using ProjectDemoRecipes.Models;
using Microsoft.EntityFrameworkCore;


public class RecipeRepository : IRecipeRepository
{
    private readonly ApplicationDbContext _context;

    public RecipeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Recipe>> GetRecipesAsync(string? searchQuery, bool? isVegetarian, bool? isLactoseFree)
    {
        var recipes = _context.Recipes.AsQueryable();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            recipes = recipes.Where(r =>
                r.Title.ToLower().Contains(searchQuery.ToLower()) ||
                r.Description.ToLower().Contains(searchQuery.ToLower()));
        }

        if (isVegetarian.HasValue)
        {
            recipes = recipes.Where(r => r.IsVegetarian == isVegetarian.Value);
        }

        if (isLactoseFree.HasValue)
        {
            recipes = recipes.Where(r => r.IsLactoseFree == isLactoseFree.Value);
        }

        return await recipes.ToListAsync();
    }

    public async Task<Recipe?> GetRecipeByIdAsync(int id)
    {
        return await _context.Recipes.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task AddRecipeAsync(Recipe recipe)
    {
        await _context.Recipes.AddAsync(recipe);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRecipeAsync(Recipe recipe)
    {
        _context.Recipes.Update(recipe);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRecipeAsync(int id)
    {
        var recipe = await GetRecipeByIdAsync(id);
        if (recipe != null)
        {
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> RecipeExistsAsync(int id)
    {
        return await _context.Recipes.AnyAsync(r => r.Id == id);
    }
}
