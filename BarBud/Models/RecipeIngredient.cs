using System.ComponentModel.DataAnnotations;

namespace BarBud.Models;

public class RecipeIngredient
{
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;
    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = null!;

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Quantity { get; set; } = 0;
    public string Unit { get; set; } = string.Empty;
}
