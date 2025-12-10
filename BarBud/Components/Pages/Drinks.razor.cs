using Microsoft.AspNetCore.Components;
using MudBlazor;
using BarBud.Models;
using BarBud.Interfaces;
using BarBud.Components.Shared;

namespace BarBud.Components.Pages;

public partial class Drinks : ComponentBase
{
    [Inject] private IIngredientServices IngredientService { get; set; } = null!;
    [Inject] private IDrinkServices DrinkService { get; set; } = null!;
    [Inject] private IRecipeServices RecipeService { get; set; } = null!;
    [Inject] private IRecipeBuilder RecipeBuilder { get; set; } = null!;
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

    private string _searchString = string.Empty;
    private string newDrinkName = string.Empty;
    private bool createRecipe = false;
    private List<Ingredient> availableIngredients = new();
    private RecipeForm? recipeForm;

    protected override async Task OnInitializedAsync()
    {
        await LoadDrinksAsync();
        availableIngredients = await IngredientService.GetAllIngredientsAsync();
    }

    public async Task LoadDrinksAsync()
    {
        _drinkList = await DrinkService.GetAllDrinksAsync();
        filteredDrinkList = _drinkList;
    }

    public async Task AddDrinkAsync()
    {
        ErrorMessage = null;
        SuccessMessage = null;

        try
        {
            // Validate drink name
            if (string.IsNullOrWhiteSpace(newDrinkName))
            {
                Name = newDrinkName.Trim(),
                TempUserID = 1
            };

            // Validate recipe if creating one
            if (createRecipe && recipeForm != null && !recipeForm.Validate())
            {
                ErrorMessage = "Please fill in all required recipe fields";
                return;
            }

            // Create drink
            var drink = new Drink { Name = newDrinkName.Trim() };
            var createdDrink = await DrinkService.AddAsync(drink);

            // Create recipe using RecipeBuilder
            if (createRecipe && recipeForm != null && createdDrink is not null)
            {
                var validIngredients = recipeForm.GetValidIngredients()
                    .Select(i => new RecipeIngredientInput
                    {
                        IngredientId = i.IngredientId!.Value,
                        Quantity = i.Quantity,
                        Unit = i.Unit ?? string.Empty
                    });

                await RecipeBuilder
                    .ForDrink(createdDrink)
                    .WithName(recipeForm.RecipeName)
                    .WithInstructions(recipeForm.Instructions)
                    .AddIngredients(validIngredients)
                    .BuildAndSaveAsync();

                SuccessMessage = $"Drink '{newDrinkName}' and recipe created successfully!";
                Snackbar.Add(SuccessMessage, Severity.Success);
            }
            else
            {
                 SuccessMessage = $"Drink '{newDrinkName}' created successfully!";
                Snackbar.Add(SuccessMessage, Severity.Success);
            }

            // Reset form
            ResetForm();
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
        Snackbar.Add("Drink deleted successfully!", Severity.Info);
    }

    public async Task UpdateDrinkAsync(Drink drink)
    {
        await DrinkService.UpdateAsync(drink);
        await LoadDrinksAsync();
        filteredDrinkList = _drinkList;
        Snackbar.Add("Drink updated successfully!", Severity.Success);
    }

    private void ResetForm()
    {
        newDrinkName = string.Empty;
        createRecipe = false;
        recipeForm?.Reset();
    }
}
