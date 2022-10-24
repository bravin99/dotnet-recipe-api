using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_recipe_api.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public virtual List<Ingredient>? Ingredients { get; set; }
        [Required]
        public string? Directions { get; set; }
        [Required]
        public int NumberOfServings { get; set; }
        [Required]
        public double CaloriesPerServing { get; set; }
        [Required]
        public string? CookTime { get; set; }
        [Required]
        public string? WrittenBy { get; set; }
        [Required]
        public bool Published { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

    public class Ingredient
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public string? Measurement { get; set; }

    }

}