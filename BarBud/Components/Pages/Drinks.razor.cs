using Microsoft.AspNetCore.Components;
using MudBlazor;
using BarBud.Models;
using BarBud.Services;
using BarBud.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BarBud.Components.Pages
{
    [Authorize]
    public partial class Drinks : ComponentBase
    {
        [Inject] private IDrinkServices DrinkService { get; set; } = null!;
        private List<Drink> _drinkList = new();
       private List<Drink> filteredDrinkList = new();
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
        protected string? ErrorMessage { get; set; }
        // Bind input to a simple value to avoid constructing an invalid Drink
        private string newDrinkName = string.Empty;
        private MudForm? form;

        protected override async Task OnInitializedAsync()
        {
            await LoadDrinksAsync();
        }

        public async Task LoadDrinksAsync()
        {
            _drinkList = await DrinkService.GetAllDrinksAsync();
        }

        // Minimal add: create Drink with required Name only
        public async Task AddDrinkAsync()
        {
            ErrorMessage = null;

            if (form is not null)
            {
                await form.Validate();
                if (!form.IsValid)
                {
                    ErrorMessage = "Please fix validation errors.";
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(newDrinkName))
            {
                ErrorMessage = "Drink name is required.";
                return;
            }

            var drink = new Drink
            {
                Name = newDrinkName.Trim()
                // Description optional; Recipes can be added later
            };

            await DrinkService.AddAsync(drink);

            newDrinkName = string.Empty;
            await LoadDrinksAsync();
        }

        public async Task DeleteDrinkAsync(int id)
        {
            await DrinkService.DeleteAsync(id);
            await LoadDrinksAsync();
            _drinkList = await DrinkService.GetAllDrinksAsync();


        }

        public async Task UpdateDrinkAsync(Drink drink)
        {
            await DrinkService.UpdateAsync(drink);
            await LoadDrinksAsync();
            filteredDrinkList = _drinkList;

        }

        private void OnInvalidSubmit()
        {
            ErrorMessage = "Please fix validation errors.";
        }
    }
}
