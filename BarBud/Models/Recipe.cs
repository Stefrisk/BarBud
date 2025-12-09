using System.ComponentModel.DataAnnotations;

namespace BarBud.Models;

public class Recipe
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int DrinkId { get; set; }
    public required Drink Drink { get; set; }
    //User
    public string UserId { get; set; } = default!;

    public User user { get; set; } = default!;
    public ICollection<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
    public string Instructions { get; set; } = string.Empty;
}
