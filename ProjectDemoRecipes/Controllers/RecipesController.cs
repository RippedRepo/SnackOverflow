using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectDemoRecipes.Data;
using ProjectDemoRecipes.Models;

namespace ProjectDemoRecipes.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipesController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        // GET: Recipes
        public async Task<IActionResult> Index(string? searchQuery, bool? isVegetarian, bool? isLactoseFree)
        {
            var recipes = await _recipeRepository.GetRecipesAsync(searchQuery, isVegetarian, isLactoseFree);
            return View(recipes);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var recipe = await _recipeRepository.GetRecipeByIdAsync(id.Value);
            if (recipe == null) return NotFound();

            return View(recipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,CookingTime,IsVegetarian,IsLactoseFree,ImageUrl")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                await _recipeRepository.AddRecipeAsync(recipe);
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var recipe = await _recipeRepository.GetRecipeByIdAsync(id.Value);
            if (recipe == null) return NotFound();

            return View(recipe); // Make sure this returns the correct view
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Recipe recipe)
        {
            if (id != recipe.Id) return NotFound();
            if (!ModelState.IsValid) return View(recipe);

            if (!await _recipeRepository.RecipeExistsAsync(id)) return NotFound();

            await _recipeRepository.UpdateRecipeAsync(recipe);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var recipe = await _recipeRepository.GetRecipeByIdAsync(id.Value);
            if (recipe == null) return NotFound();

            return View(recipe); // Ensure this returns the correct view
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _recipeRepository.DeleteRecipeAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
