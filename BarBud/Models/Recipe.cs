using System.ComponentModel.DataAnnotations;

namespace BarBud.Models;

public class Recipe
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public required Drink Drink { get; set; }
    public int DrinkId { get; set; }
    public ICollection<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
    public string Instructions { get; set; } = string.Empty;
}
