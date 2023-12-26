using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipe.API.Data;
using Recipe.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CategoriesController(MyDbContext context)
        {
            _context = context;
        }

        // Endpoint: /api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categories>>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }

        // Endpoint: /api/categories/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Categories>> GetCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound(); // 404 Not Found indien niet gevonden
            }

            return Ok(category);
        }

        // Endpoint: /api/categories
        [HttpPost]
        public async Task<ActionResult<Categories>> CreateCategory(Categories category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            // Return 201 Created met de locatie van de nieuwe resource
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }
    }
}
