using Microsoft.AspNetCore.Components;
using BarBud.Models;
using MudBlazor;
using BarBud.Services;


namespace BarBud.Components.Pages;

public partial class Ingredients : ComponentBase
{
    [Inject] private IngredientServices IngredientService { get; set; } = null;

    private List<Ingredient> _ingredientsList = new();
    private List<Ingredient> filteredIngredientsList = new();

    private string SearchString
    {
        get => _searchString;
        set
        {
            _searchString = value;
            filteredIngredientsList = filteredIngredientsList
                .Where(i => i.Name.Contains(value, StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (string.IsNullOrWhiteSpace(value)) filteredIngredientsList = _ingredientsList;
        }
    }

    private string _searchString { get; set; }
    protected string? ErrorMessage { get; set; }
    private Ingredient NewIngredient = new();
    private MudForm? form;


    protected override async Task OnInitializedAsync()
    {
        await LoadIngredientsAsync();
        filteredIngredientsList = _ingredientsList;
    }


    private async Task LoadIngredientsAsync()
    {
        _ingredientsList = await IngredientService.GetAllIngredientsAsync();
    }

    private async Task AddIngredientAsync(Ingredient newIngredient)
    {
        ErrorMessage = null;

        if (string.IsNullOrWhiteSpace(newIngredient.Name))
        {
            ErrorMessage = "An Ingredient name is required";
            return;
        }


        await IngredientService.AddAsync(newIngredient);

        await LoadIngredientsAsync();
    }

    private async Task OnRowClick(Ingredient ingredient)
    {
        var parameters = new DialogParameters
        {
            ["Ingredient"] = ingredient
        };
        // Kolla på detta sen---
        var dialog = DialogService.Show<EditIngredientDialog>("Edit Ingredient", parameters);
        var result = await dialog.Result;

        if (!result!.Canceled)
        {
            var updated = (Ingredient)result.Data!;

            await IngredientService.UpdateAsync(updated);

            _ingredientsList = await IngredientService.GetAllIngredientsAsync();
        }
    }

    protected async Task DeleteIngredientAsync(int id)
    {
        await IngredientService.DeleteAsync(id);

        await LoadIngredientsAsync();
        filteredIngredientsList = _ingredientsList;
    }
}