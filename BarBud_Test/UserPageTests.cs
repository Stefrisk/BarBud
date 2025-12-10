using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using BarBud.Components;
using BarBud.Interfaces;
using BarBud.Models;
using Moq;
using Xunit;

namespace BarBud_Test
{
    public class UserPageTests
    {
        private readonly List<Drink> _fakeUserDrinks = new()
        {
           new Drink { Id = 1, Name = "Cognac" },
            new Drink { Id = 2, Name = "Whiskey" }

        };
        private readonly List<Ingredient> _fakeUserIngredients = new()
        {
           new Ingredient { Id = 19, Name = "Socker" },
            new Ingredient { Id = 20, Name = "Körsbär" }
        };
        [Fact]
        public async Task GetAllDrinksWithDetailsAsync_ShouldReturnAllDrinksForUser()
        {
            //Arrange
            var loggedInUserId = 1;
            var mockDrinkService = new Mock<IDrinkServices>();
            mockDrinkService.Setup(service => service.GetAllDrinksWithDetailsAsync(loggedInUserId)).ReturnsAsync(_fakeUserDrinks);
            //act
            var actual = await mockDrinkService.Object.GetAllDrinksWithDetailsAsync(loggedInUserId);

            //Assert
            Assert.Equal(_fakeUserDrinks.Count, actual.Count);
            Assert.Contains(actual, d => d.Name == "Cognac");
            Assert.Contains(actual, d => d.Name == "Whiskey");
        }
        [Fact]
        public async Task GetByUsedIdAsync_ShouldReturnAllIngredientsForUser()
        {
            //Arrange
            var loggedInUserId = 1;
            var mockIngredientService = new Mock<IIngredientServices>();
            mockIngredientService.Setup(service => service.GetByUserIdAsync(loggedInUserId)).ReturnsAsync(_fakeUserIngredients);
            //act
            var actual = await mockIngredientService.Object.GetByUserIdAsync(loggedInUserId);

            //Assert
            Assert.Equal(_fakeUserIngredients.Count, actual.Count);
            Assert.Contains(actual, i => i.Name == "Socker");
            Assert.Contains(actual, i => i.Name == "Körsbär");
        }
       
    }
}
