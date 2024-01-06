using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recipe.API.Data;
using Recipe.API.Models;
using static Recipe.API.Models.Dto.RecipesDTO;

namespace Recipe.API
{
    [Route("api/recipe")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly MyDbContext _context;

        public RecipeController(MyDbContext context)
        {
            _context = context;
        }

        // Endpoint: /api/recipe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipesBasic>>> GetRecipes()
        {
            var recipes = await _context.Recipes
                .Select(r => new RecipesBasic
                {
                    Id = r.Id,
                    Title = r.Title,
                    Time = r.Time,
                    Category = r.Category.Name,
                    Difficulty = r.Difficulty
                })
                .ToListAsync();

            return Ok(recipes);
        }

        // Endpoint: /api/recipe/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetRecipeById(int id)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.Ingredients)
                .SingleOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            var recipeDTO = new RecipesDetail
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Difficulty = recipe.Difficulty,
                Time = recipe.Time,
                Category = recipe.Category.Name,
                Ingredients = recipe.Ingredients.Select(i => new IngredientDTO
                {
                    Id = i.Id,
                    Name = i.Name,
                    Quantity = i.Quantity,
                    Unit = i.Unit
                }).ToList()
            };

            return Ok(recipeDTO);
        }

        // Endpoint: /api/recipe/
        [HttpPost]
        public async Task<ActionResult<Recipes>> CreateRecipe(Recipes recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecipeById), new { id = recipe.Id }, recipe);
        }

        // Endpoint: /api/recipe/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Recipes>> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return Ok(recipe);
        }
        // Voeg toe aan RecipeController.cs

        // Endpoint: /api/recipe/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<RecipesBasic>>> Search([FromQuery] RecipeSearchOptions options)
        {
            var query = _context.Recipes.AsQueryable();

            // Filter op zoekterm
            if (!string.IsNullOrEmpty(options.SearchTerm))
            {
                query = query.Where(r => r.Title.Contains(options.SearchTerm));
            }

            // Filter op categorieën
            if (options.Categories != null && options.Categories.Any())
            {
                query = query.Where(r => options.Categories.Contains(r.CategoryId));
            }

            // Filter op maximale moeilijkheidsgraad
            if (options.MaxDifficulty.HasValue)
            {
                query = query.Where(r => (int)r.Difficulty <= options.MaxDifficulty);
            }

            // Filter op maximale bereidingstijd
            if (options.MaxTime.HasValue)
            {
                query = query.Where(r => r.Time <= options.MaxTime);
            }

            var result = await query
                .Select(r => new RecipesBasic
                {
                    Id = r.Id,
                    Title = r.Title,
                    Time = r.Time,
                    Category = r.Category.Name,
                    Difficulty = r.Difficulty
                })
                .ToListAsync();

            return Ok(result);
        }

    }

}