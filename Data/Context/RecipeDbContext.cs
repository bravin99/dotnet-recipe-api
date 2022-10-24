using Microsoft.EntityFrameworkCore;
using dotnet_recipe_api.Models;

namespace dotnet_recipe_api.Data.Context
{
    public class RecipeDbContext : DbContext
    {
        public RecipeDbContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Recipe>? Recipes {get; set; }
        public DbSet<Ingredient>? Ingredients {get; set;}
    }
}