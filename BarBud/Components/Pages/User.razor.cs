using BarBud.Interfaces;
using BarBud.Models;
using Microsoft.AspNetCore.Components;

namespace BarBud.Components.Pages;

public partial class User : ComponentBase
{
    [Inject] public IDrinkServices DrinkService { get; set; }
    [Inject] public IIngredientServices IngredientService { get; set; }
    public List<Drink> Drinks { get; set; } = new();
    public List<Ingredient> Ingredients { get; set; }
    protected override async Task OnInitializedAsync()
    {
        Drinks = await DrinkService.GetAllDrinksWithDetailsAsync();
        
        Ingredients = (await IngredientService.GetAllIngredientsAsync()).ToList();
    }
}