using Microsoft.AspNetCore.Components;
using BarBud.Db;
using BarBud.Models;
using MudBlazor;
using BarBud.Services;
namespace BarBud.Components.Pages;

public partial class Ingredients : ComponentBase
{
    [Inject] IngredientFunctions IngredientService { get; set; } = default!;
    
    private List<Ingredient> IngredientList = new();
    protected string? ErrorMessage { get; set; }
    private Ingredient NewIngredient = new();

    protected override async Task OnInitializedAsync()
    {
        await LoadIngredientsAsync();
    }
    protected async Task LoadIngredientsAsync() 
    { 
        IngredientList = await IngredientService.GetAllIngredientsAsync();
    }
    protected async Task AddIngredientAsync(Ingredient newIngredient) 
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
    
    protected async Task DeleteIngredientAsync(int id)
    {
        await IngredientService.DeleteAsync(id);
        await LoadIngredientsAsync();
    }
    protected async Task UpdateIngredientAsync(Ingredient ingredient)
    {
        await IngredientService.UpdateAsync(ingredient);
        await LoadIngredientsAsync();
    }
}