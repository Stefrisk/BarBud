using Microsoft.AspNetCore.Components;
using MudBlazor;
using BarBud.Models;
using BarBud.Services;
using BarBud.Interfaces;

namespace BarBud.Components.Pages;

public partial class Drinks : ComponentBase
{
    [Inject] private IIngredientServices IngredientService { get; set; } = null!;
    [Inject] private IDrinkServices DrinkService { get; set; } = null!;
    [Inject] private IRecipeServices RecipeService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    private List<Drink> _drinkList = new();
    private List<Drink> filteredDrinkList = new();
    protected string? ErrorMessage { get; set; }
    protected string? SuccessMessage { get; set; }
    private string SearchString
    {
        get => _searchString;
        set
        {
            _searchString = value;
            _drinkList = _drinkList
                .Where(i => i.Name.Contains(value, StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (string.IsNullOrWhiteSpace(value)) _drinkList = filteredDrinkList;
        }
    }

    private string _searchString { get; set; }

    private string newDrinkName = string.Empty;

    // Recipe creation fields
    private bool createRecipe = false;
    private string recipeName = string.Empty;
    private string recipeInstructions = string.Empty;
    private List<RecipeIngredientInput> recipeIngredients = new();
    private List<Ingredient> availableIngredients = new();

    protected override async Task OnInitializedAsync()
    {
        await LoadDrinksAsync();
        availableIngredients = await IngredientService.GetAllIngredientsAsync();
        
        // Initialize with one empty ingredient row
        if (recipeIngredients.Count == 0)
        {
            recipeIngredients.Add(new RecipeIngredientInput());
        }
    }

    public async Task LoadDrinksAsync()
    {
        _drinkList = await DrinkService.GetAllDrinksAsync();
    }

    private void OnCreateRecipeToggle(bool value)
    {
        createRecipe = value;
        if (createRecipe && recipeIngredients.Count == 0)
        {
            recipeIngredients.Add(new RecipeIngredientInput());
        }
        else if (!createRecipe)
        {
            // Clear recipe fields when toggled off
            recipeName = string.Empty;
            recipeInstructions = string.Empty;
            recipeIngredients.Clear();
        }
    }

    private void AddIngredientRow()
    {
        recipeIngredients.Add(new RecipeIngredientInput());
    }

    private void RemoveIngredientRow(RecipeIngredientInput ingredient)
    {
        recipeIngredients.Remove(ingredient);
    }

    public async Task AddDrinkAsync()
    {
        ErrorMessage = null;
        SuccessMessage = null;

        try
        {
            // Create drink
            var drink = new Drink
            {
                Name = newDrinkName.Trim()
            };

            var createdDrink = await DrinkService.AddAsync(drink);

            // Create recipe if toggle is enabled
            if (createRecipe && createdDrink is not null)
            {
                var recipe = new Recipe
                {
                    Name = recipeName.Trim(),
                    DrinkId = createdDrink.Id,
                    Drink = createdDrink,
                    Instructions = recipeInstructions.Trim(),
                    Ingredients = recipeIngredients
                        .Where(i => i.IngredientId.HasValue)
                        .Select(i => new RecipeIngredient
                        {
                            IngredientId = i.IngredientId!.Value,
                            Quantity = i.Quantity,
                            Unit = i.Unit ?? string.Empty
                        }).ToList()
                };

                await RecipeService.AddAsync(recipe);
                SuccessMessage = $"Drink '{newDrinkName}' and recipe '{recipeName}' created successfully!";
                Snackbar.Add(SuccessMessage, Severity.Success);
            }
            else
            {
                SuccessMessage = $"Drink '{newDrinkName}' created successfully!";
                Snackbar.Add(SuccessMessage, Severity.Success);
            }

            // Reset form
            newDrinkName = string.Empty;
            recipeName = string.Empty;
            recipeInstructions = string.Empty;
            recipeIngredients.Clear();
            createRecipe = false;

            await LoadDrinksAsync();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error creating drink: {ex.Message}";
            Snackbar.Add(ErrorMessage, Severity.Error);
        }
    }

    public async Task DeleteDrinkAsync(int id)
    {
        await DrinkService.DeleteAsync(id);
        await LoadDrinksAsync();
        _drinkList = await DrinkService.GetAllDrinksAsync();
        Snackbar.Add("Drink deleted successfully!", Severity.Info);
    }

    public async Task UpdateDrinkAsync(Drink drink)
    {
        await DrinkService.UpdateAsync(drink);
        await LoadDrinksAsync();
        filteredDrinkList = _drinkList;
        Snackbar.Add("Drink updated successfully!", Severity.Success);
    }

    // Helper class for recipe ingredient input
    public class RecipeIngredientInput
    {
        public int? IngredientId { get; set; }
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }
    }
}
