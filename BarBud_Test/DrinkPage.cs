using System;
using System.Collections.Generic;
using System.Text;
using BarBud.Models;
using BarBud.Interfaces;
using Xunit;
using BarBud.Services;
using Moq;
namespace BarBud_Test
{
   

    public class DrinkPage
    {
        private List<Drink> _fakeDrinks = new()
        {
            new Drink { Name = "Mojito", Description = "Classic Cuban cocktail" },
            new Drink { Name = "Martini", Description = "Gin and vermouth" },
            new Drink { Name = "Margarita", Description = "Tequila, lime juice, and triple sec" }
        };
        [Fact]
        public async Task GetAllDrinks_ShouldReturnAllDrinks()
        {
            
        }
    }
}
