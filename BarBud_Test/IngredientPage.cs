using BarBud.Interfaces;
using BarBud.Services;
using BarBud.Models;
using Moq;

namespace BarBud_Test;

public class IngredientPage
{
    private List<Ingredient> _fakeIngredients = new()
    {
        new Ingredient { Name = "Rum", Description = "light" },
        new Ingredient { Name = "Pomegranate", Description = "lots of seeds" },
        new Ingredient { Name = "Grenadine", Description = "red" }
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


        // Act


        // Assert
    }
}