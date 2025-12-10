namespace BarBud.Models;
/// <summary>
/// Input model for recipe ingredients used in forms and UI
/// </summary>
public class RecipeIngredientInput
{
    public int? IngredientId { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;    
}
