using BarBud.Interfaces;
using BarBud.Services;
using BarBud.Models;
using Moq;

namespace BarBud_Test;

public class IngredientPage
{
    private List<Ingredient> _fakeIngredients = new()
    {
        new Ingredient { Id = 1 , Name = "Rum", Description = "light" },
        new Ingredient {Id = 2, Name = "Pomegranate", Description = "lots of seeds" },
        new Ingredient {Id = 3, Name = "Grenadine", Description = "red" }
    };

    [Fact]
    public async Task GetAllIngredients_ShouldReturnAllIngredients()
    {
        // Arrange
        var mockService = new Mock<IIngredientServices>();
        mockService.Setup(s => s.GetAllIngredientsAsync()).ReturnsAsync(_fakeIngredients);

        // Act
        var result = await mockService.Object.GetAllIngredientsAsync();

        // Assert
        Assert.Equal(_fakeIngredients, result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddIngredientToList()
    {
        // Arrange
        var ingredient = new Ingredient { Name = "grapes", Description = "red" };
        var mockService = new Mock<IIngredientServices>();
        mockService.Setup(s => s.AddAsync(It.IsAny<Ingredient>()))
            .ReturnsAsync(_fakeIngredients)
            .Callback<Ingredient>(i => _fakeIngredients.Add(i));


        // Act
        var result = await mockService.Object.AddAsync(ingredient);

        // Assert
        Assert.Equal(_fakeIngredients, result);
        Assert.Contains(ingredient.Name, result.Last().Name);
    }

    [Fact]
    public async Task Delete_ShouldRemoveIngredientFromList()
    {
        // Arrange
        var id = 1;
        var mockService = new Mock<IIngredientServices>();
        mockService.Setup(s => s.DeleteAsync(It.IsAny<Int32>()))
            .ReturnsAsync(_fakeIngredients)
            .Callback<Int32>(id => _fakeIngredients.RemoveAll(i => i.Id == id));
        // Act
        var result = await mockService.Object.DeleteAsync(id);

        // Assert
        Assert.DoesNotContain(id, result.Select(i => i.Id));
    }

    public async Task UpdateAsync_ShouldUpdateIngredientInList()
    {
        // Arrange

        //act

        //assert
    }
}