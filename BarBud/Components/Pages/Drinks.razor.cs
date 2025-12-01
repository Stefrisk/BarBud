using Microsoft.AspNetCore.Components;
using MudBlazor;
using BarBud.Models;
using BarBud.Services;
using System.Runtime.CompilerServices;

namespace BarBud.Components.Pages
{
    public partial class Drinks : ComponentBase
    {
        [Inject] private DrinkFunctions DrinkService { get; set; } = null!;
        private List<Drink> _drinkList = new();
        protected string? ErrorMessage { get; set; }
        private Drink newDrink = new();
        private MudForm? form;

        protected override async Task OnInitializedAsync()
        {
            await LoadDrinksAsync();
        }
        public async Task LoadDrinksAsync()
        {
            _drinkList = await DrinkService.GetAllDrinksAsync();
        }
        public async Task AddDrinkAsync(Drink newDrink)
        {
            ErrorMessage = null;
            if (string.IsNullOrWhiteSpace(newDrink.Name))
            {
                ErrorMessage = "Drink name cannot is required.";
                return;
            }
            await DrinkService.AddAsync(newDrink);
            await LoadDrinksAsync();
        }
        public async Task DeleteDrinkAsync(int id)
        {
            await DrinkService.DeleteAsync(id);
            await LoadDrinksAsync();
        }
        public async Task UpdateDrinkAsync(Drink drink)
        {
            await DrinkService.UpdateAsync(drink);
            await LoadDrinksAsync();
        }
    }
}
