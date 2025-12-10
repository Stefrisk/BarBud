using BarBud.Interfaces;
using BarBud.Models;
using Microsoft.AspNetCore.Components;

namespace BarBud.Components.Pages;

public partial class User : ComponentBase
{
    [Inject] public IDrinkServices DrinkService { get; set; }
    [Inject] public IIngredientServices IngredientService { get; set; }
    private List<Drink> Drinks { get; set; } = new();
    private List<Ingredient> Ingredients { get; set; } = new();
    private int LoggedInUserId = 1;
    protected override async Task OnInitializedAsync()
    {
        Drinks = await DrinkService.GetAllDrinksWithDetailsAsync(LoggedInUserId);
        
        Ingredients = await IngredientService.GetByUserIdAsync(LoggedInUserId);
    }
}