using System.ComponentModel.DataAnnotations;

namespace BarBud.Models;

public class Ingredient
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
   

    // Navigation property for the many-to-many relationship with Recipe
    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}