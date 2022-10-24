using dotnet_recipe_api.Models;
using dotnet_recipe_api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeDbContext _context;
        public RecipeController(RecipeDbContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public async Task<ActionResult<Recipe[]>> GetRecipes()
        {
            var results = await _context.Recipes!.Include(r => r.Ingredients).ToListAsync();
            if (results == null)
                return NotFound();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipeById(int id)
        {
            var recipe = await _context.Recipes!.Include(r => r.Ingredients).FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
                return NotFound();
            return Ok(recipe);
        }

        [HttpPost("new")]
        public async Task<ActionResult<string>> AddNewRecipe([FromBody]Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                await _context.Recipes!.AddAsync(recipe);
                await _context.SaveChangesAsync();
                return Ok($"Recipe {recipe.Name} created successfully!");
            }
            return BadRequest("An error occured while saving your recipe");
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<string>> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes!.FirstOrDefaultAsync(r => r.Id == id);
            
            if (recipe != null)
            {
                _context.Recipes!.Remove(recipe);
                await _context.SaveChangesAsync();
                return Ok($"Recipe {recipe.Name} deleted successfully!");
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ingredient>> GetIngredientById(int id)
        {
            var ing = await _context.Ingredients!.FirstOrDefaultAsync(i => i.Id == id);
            if (ing == null)
                return NotFound();
            return Ok(ing);
        }

        [HttpPost("add-ingredient/{id}")]
        public async Task<ActionResult<string>> AddTopping(int id, [FromBody]Ingredient value)
        {
            var newIngredient = new Ingredient(){
                Name = value.Name,
                RecipeId = id,
                Description = value.Description,
                Measurement = value.Measurement
            };
            if (ModelState.IsValid)
            {
                await _context.Ingredients!.AddAsync(newIngredient);
                await _context.SaveChangesAsync();
                return Ok($"Ingredient {newIngredient.Name} added to recipe with id {id}");
            }
            return BadRequest();
        }

        [HttpDelete("delete/ingredient/{id}")]
        public async Task<ActionResult<string>> DeleteIngredient(int id)
        {
            var ing = await _context.Ingredients!.FirstOrDefaultAsync(r => r.Id == id);
            if (ing != null)
            {
                _context.Ingredients!.Remove(ing);
                await _context.SaveChangesAsync();
                return Ok($"Ingredient deleted succesfully");
            }
            return BadRequest();
        }


    }
}