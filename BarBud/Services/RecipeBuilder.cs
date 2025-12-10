using BarBud.Models;
using BarBud.Interfaces;

namespace BarBud.Services;

/// <summary>
/// Builder service for creating Recipe objects with a fluent API
/// Decouples recipe creation logic from the UI and provides validation
/// </summary>
public class RecipeBuilder : IRecipeBuilder
{
    private readonly IRecipeServices _recipeService;
    private Recipe _recipe;
    private List<RecipeIngredient> _ingredients;

    public RecipeBuilder(IRecipeServices recipeService)
    {
        _recipeService = recipeService ?? throw new ArgumentNullException(nameof(recipeService));
        Reset();
    }

    /// <summary>
    /// Sets the drink for the recipe using a Drink object
    /// </summary>
    public IRecipeBuilder ForDrink(Drink drink)
    {
        if (drink == null)
            throw new ArgumentNullException(nameof(drink), "Drink cannot be null");

        if (drink.Id <= 0)
            throw new ArgumentException("Drink must have a valid Id", nameof(drink));

        _recipe.DrinkId = drink.Id;
        _recipe.Drink = drink;
        
        return this;
    }

    /// <summary>
    /// Sets the drink for the recipe using a drink ID
    /// Useful when you only have the ID and not the full Drink object
    /// </summary>
    public IRecipeBuilder ForDrink(int drinkId)
    {
        if (drinkId <= 0)
            throw new ArgumentException("DrinkId must be greater than 0", nameof(drinkId));

        _recipe.DrinkId = drinkId;
        // Note: Drink entity will need to be set separately or loaded from DB
        
        return this;
    }

    /// <summary>
    /// Sets the name of the recipe
    /// </summary>
    public IRecipeBuilder WithName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Recipe name cannot be empty", nameof(name));

        _recipe.Name = name.Trim();
        
        return this;
    }

    /// <summary>
    /// Sets the instructions for the recipe
    /// </summary>
    public IRecipeBuilder WithInstructions(string instructions)
    {
        _recipe.Instructions = instructions?.Trim() ?? string.Empty;
        
        return this;
    }

    /// <summary>
    /// Adds a single ingredient to the recipe
    /// </summary>
    public IRecipeBuilder AddIngredient(int ingredientId, decimal quantity, string unit)
    {
        if (ingredientId <= 0)
            throw new ArgumentException("IngredientId must be greater than 0", nameof(ingredientId));

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than 0", nameof(quantity));

        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentException("Unit cannot be empty", nameof(unit));

        _ingredients.Add(new RecipeIngredient
        {
            IngredientId = ingredientId,
            Quantity = quantity,
            Unit = unit.Trim()
        });

        return this;
    }

    /// <summary>
    /// Adds multiple ingredients to the recipe from a collection
    /// </summary>
    public IRecipeBuilder AddIngredients(IEnumerable<RecipeIngredientInput> ingredients)
    {
        if (ingredients == null)
            throw new ArgumentNullException(nameof(ingredients));

        foreach (var ingredient in ingredients)
        {
            AddIngredient(ingredient.IngredientId, ingredient.Quantity, ingredient.Unit);
        }

        return this;
    }

    /// <summary>
    /// Builds the recipe object without saving to database
    /// Validates that all required fields are set
    /// </summary>
    public Recipe Build()
    {
        ValidateRecipe();
        
        // Assign ingredients to recipe
        _recipe.Ingredients = _ingredients;
        
        // Return the built recipe
        var builtRecipe = _recipe;
        
        // Reset builder for next use
        Reset();
        
        return builtRecipe;
    }

    /// <summary>
    /// Builds the recipe and saves it to the database
    /// </summary>
    public async Task<Recipe> BuildAndSaveAsync()
    {
        ValidateRecipe();
        
        // Assign ingredients to recipe
        _recipe.Ingredients = _ingredients;
        
        // Save to database
        var savedRecipe = await _recipeService.AddAsync(_recipe);
        
        // Reset builder for next use
        Reset();
        
        return savedRecipe;
    }

    /// <summary>
    /// Resets the builder to initial state for reuse
    /// </summary>
    public void Reset()
    {
        _recipe = new Recipe 
        { 
            Drink = null! // Will be set via ForDrink
        };
        _ingredients = new List<RecipeIngredient>();
    }

    /// <summary>
    /// Validates that the recipe has all required fields before building
    /// </summary>
    private void ValidateRecipe()
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(_recipe.Name))
            errors.Add("Recipe name must be set");

        if (_recipe.DrinkId <= 0)
            errors.Add("Drink must be set");

        if (_ingredients.Count == 0)
            errors.Add("Recipe must have at least one ingredient");

        if (errors.Any())
        {
            throw new InvalidOperationException(
                $"Cannot build recipe. Validation errors: {string.Join(", ", errors)}");
        }
    }
}