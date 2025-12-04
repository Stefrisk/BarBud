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
            new Drink { Id = 2,Name = "Mojito", Description = "Classic Cuban cocktail" },
            new Drink { Name = "Martini", Description = "Gin and vermouth" },
            new Drink { Name = "Old Fashioned", Description = "Whiskey Cocktail" }
        };
        [Fact]
        public async Task GetAllDrinks_ShouldReturnAllDrinks()
        {
            //Arrange
            var mockService = new Mock<IDrinkServices>();
            mockService.Setup(service => service.GetAllDrinksAsync())
                       .ReturnsAsync(_fakeDrinks);
            //Act
            var actual = await mockService.Object.GetAllDrinksAsync();

            //Assert
            Assert.Equal(_fakeDrinks.Count, actual.Count);
        }
        [Fact]
        public async Task AddAsync_ShouldAddDrinkToList()
        {
            //Arrange
            var drink = new Drink { Name = "Old Fashioned", Description = "Whiskey cocktail" };
            var drinks = _fakeDrinks;
            var mockService = new Mock<IDrinkServices>();
            mockService.Setup(service => service.AddAsync(It.IsAny<Drink>()))
            .ReturnsAsync((Drink d) =>
            {
                drinks.Add(d);
                return drinks;
            });
            //Act
            var actual = await mockService.Object.AddAsync(drink);

            //Assert
            Assert.Equal(drinks, actual);
            Assert.Contains(actual, d => d.Name == drink.Name && d.Description == drink.Description);
        }
        [Fact]
        public async Task Delete_ShouldRemoveDrinkFromList()
        {
            //Arrange
            var drinkIdToDelete = 1;
            var drinks = new List<Drink>
            {
                new Drink { Id = 1, Name = "GROGG", Description = "Fan va gött" },
                new Drink { Id = 2, Name = "ÖLLL", Description = "ÖÖÖÖÖÖÖÖÖÖÖÖLLLLLLLL" },
            };
            var mockService = new Mock<IDrinkServices>();
            mockService.Setup(service => service.DeleteAsync(drinkIdToDelete))
            .ReturnsAsync(() =>
            {
                var drinkToRemove = drinks.Find(d => d.Id == drinkIdToDelete);
                if (drinkToRemove != null)
                {
                    drinks.Remove(drinkToRemove);
                    return true;
                }
                return false;
            });
            //Act
            var actual = await mockService.Object.DeleteAsync(drinkIdToDelete);
            //Assert
            Assert.True(actual);
            Assert.DoesNotContain(drinks, d => d.Id == drinkIdToDelete);
        }
        [Fact]
        public async Task Update_ShouldUpdateDrinkInList()
        {
            //Arrange
            var drinksToUpdate = new List<Drink>(_fakeDrinks);
            var idToUpdate = 2;
            var mockService = new Mock<IDrinkServices>();
            mockService.Setup(service => service.UpdateAsync(It.IsAny<Drink>()))
                .ReturnsAsync((Drink d) =>
            {
                var drinkToUpdate = drinksToUpdate.Find(drink => drink.Id == d.Id);
                if (drinkToUpdate != null)
                {
                    drinkToUpdate.Name = d.Name;
                    drinkToUpdate.Description = d.Description;
                    return true;
                }
                return false;
            });
            var updatedDrink = new Drink
            {
                Id = idToUpdate,
                Name = "Updated Name",
                Description = "Updated Description"
            };
            //Act
            var actual = await mockService.Object.UpdateAsync(updatedDrink);

            //Assert
            Assert.True(actual);
            Assert.Contains(drinksToUpdate,
        d => d.Id == idToUpdate
          && d.Name == "Updated Name"
          && d.Description == "Updated Description");
        }
    }
}
