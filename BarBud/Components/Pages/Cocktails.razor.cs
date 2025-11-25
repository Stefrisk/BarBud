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
        protected string? ErrorMessage { get; set; }
        private Cocktail newCocktail = new();
        private MudForm? form;

        protected override async Task OnInitializedAsync()
        {
            await LoadCocktailsAsync();
        }
        private async Task LoadCocktailsAsync()
        {
            _cocktailsList = await CocktailService.GetAllCocktailsAsync();
        }
        protected async Task AddCocktailsAsync(Cocktail newCocktail)
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
        protected async Task DeleteCocktailAsync(int id)
        {
            await CocktailService.DeleteAsync(id);
            await LoadCocktailsAsync();
        }
        protected async Task UpdateCocktailAsync(Cocktail cocktail)
        {
            await CocktailService.UpdateAsync(cocktail);
            await LoadCocktailsAsync();
        }
    }
}
