using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectDemoRecipes.Data;
using ProjectDemoRecipes.Models;

namespace ProjectDemoRecipes.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recipes
        public async Task<IActionResult> Index(string? searchQuery, bool? isVegetarian, bool? isLactoseFree)
        {
            var recipes = _context.Recipes.AsQueryable();

            // Apply filters
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

            return View(await recipes.ToListAsync());
        }


        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recipes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,CookingTime,IsVegetarian,IsLactoseFree,ImageUrl")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recipes == null) return NotFound();

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return NotFound();

            return View(recipe);
        }

        // POST: Recipes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Recipe recipe)
        {
            // Check if the recipe ID in the request matches the recipe object ID
            if (id != recipe.Id)
            {
                return NotFound();
            }

            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                // Log or debug ModelState errors if needed
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return View(recipe);
            }

            try
            {
                // Retrieve the existing recipe from the database
                var existingRecipe = await _context.Recipes
                                                   .FirstOrDefaultAsync(r => r.Id == id);

                if (existingRecipe == null)
                {
                    return NotFound();
                }

                // Update only scalar properties (exclude navigation properties like Reviews)
                existingRecipe.Title = recipe.Title;
                existingRecipe.Description = recipe.Description;
                existingRecipe.CookingTime = recipe.CookingTime;
                existingRecipe.IsVegetarian = recipe.IsVegetarian;
                existingRecipe.IsLactoseFree = recipe.IsLactoseFree;
                existingRecipe.ImageUrl = recipe.ImageUrl;

                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency issues
                if (!RecipeExists(recipe.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Redirect to the Index action after a successful edit
            return RedirectToAction(nameof(Index));
        }


        // Helper method to check if a Recipe exists
        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recipes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Recipes' is null.");
            }
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
