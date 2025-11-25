using Microsoft.AspNetCore.Components;
using BarBud.Models;
using MudBlazor;
using BarBud.Services;
namespace BarBud.Components.Pages;

public partial class Ingredients : ComponentBase
{
    [Inject] private IngredientFunctions IngredientService { get; set; } = null;
    
    private List<Ingredient> _ingredientsList = new();
    protected string? ErrorMessage { get; set; }
    private Ingredient NewIngredient = new();
    private MudForm? form;


    protected override async Task OnInitializedAsync()
    {
        await LoadIngredientsAsync();
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