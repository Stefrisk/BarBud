using BarBud.Models;

namespace BarBud.Interfaces;

/// <summary>
/// Builder interface for creating Recipe objects using a fluent API
/// </summary>
public interface IRecipeBuilder
{
    /// <summary>
    /// Sets the drink for the recipe using a Drink object
    /// </summary>
    IRecipeBuilder ForDrink(Drink drink);
    
    /// <summary>
    /// Sets the drink for the recipe using a drink ID
    /// </summary>
    IRecipeBuilder ForDrink(int drinkId);
    
    /// <summary>
    /// Sets the name of the recipe
    /// </summary>
    IRecipeBuilder WithName(string name);
    
    /// <summary>
    /// Sets the instructions for the recipe
    /// </summary>
    IRecipeBuilder WithInstructions(string instructions);
    
    /// <summary>
    /// Adds a single ingredient to the recipe
    /// </summary>
    IRecipeBuilder AddIngredient(int ingredientId, decimal quantity, string unit);
    
    /// <summary>
    /// Adds multiple ingredients to the recipe
    /// </summary>
    IRecipeBuilder AddIngredients(IEnumerable<RecipeIngredientInput> ingredients);
    
    /// <summary>
    /// Builds the recipe without saving to database
    /// </summary>
    Recipe Build();
    
    /// <summary>
    /// Builds and saves the recipe to the database
    /// </summary>
    Task<Recipe> BuildAndSaveAsync();
    
    /// <summary>
    /// Resets the builder state for reuse
    /// </summary>
    void Reset();
}

/// <summary>
/// Input model for recipe ingredients
/// </summary>
public class RecipeIngredientInput
{
    public int IngredientId { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
}