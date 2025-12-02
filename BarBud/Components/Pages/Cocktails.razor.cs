using Microsoft.AspNetCore.Components;
using MudBlazor;
using BarBud.Models;
using BarBud.Services;
using System.Runtime.CompilerServices;

namespace BarBud.Components.Pages
{
    public partial class Cocktails : ComponentBase
    {
        [Inject] private CocktailFunctions CocktailService { get; set; } = null!;
        private List<Cocktail> _cocktailsList = new();
        private List<Cocktail> filteredCocktailsList = new();
        private string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                filteredCocktailsList = filteredCocktailsList
                    .Where(i => i.Name.Contains(value, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                if (string.IsNullOrWhiteSpace(value)) filteredCocktailsList = _cocktailsList;
            }
        }

        private string _searchString { get; set; }
        protected string? ErrorMessage { get; set; }
        private Cocktail newCocktail = new();
        private MudForm? form;

        protected override async Task OnInitializedAsync()
        {
            await LoadCocktailsAsync();
            filteredCocktailsList = _cocktailsList;
        }
        public async Task LoadCocktailsAsync()
        {
            _cocktailsList = await CocktailService.GetAllCocktailsAsync();
        }
        public async Task AddCocktailsAsync(Cocktail newCocktail)
        {
            ErrorMessage = null;
            if (string.IsNullOrWhiteSpace(newCocktail.Name))
            {
                ErrorMessage = "Cocktail name cannot is required.";
                return;
            }
            await CocktailService.AddAsync(newCocktail);
            await LoadCocktailsAsync();
        }
        private async Task OnRowClick(Cocktail cocktail)
        {
            var parameters = new DialogParameters
            {
                ["Cocktail"] = cocktail
            };
           
            var dialog = DialogService.Show<EditIngredientDialog>("Edit Cocktail", parameters);
            var result = await dialog.Result;

            if (!result!.Canceled)
            {
                var updated = (Cocktail)result.Data!;

                await CocktailService.UpdateAsync(updated);

                _cocktailsList = await CocktailService.GetAllCocktailsAsync();
            }
        }
        public async Task DeleteCocktailAsync(int id)
        {
            await CocktailService.DeleteAsync(id);
            await LoadCocktailsAsync();
            filteredCocktailsList = _cocktailsList;
        }
        public async Task UpdateCocktailAsync(Cocktail cocktail)
        {
            await CocktailService.UpdateAsync(cocktail);
            await LoadCocktailsAsync();
            filteredCocktailsList = _cocktailsList;
        }
    }
}
