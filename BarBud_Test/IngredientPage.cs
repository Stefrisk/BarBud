using BarBud.Interfaces;
using BarBud.Services;
using BarBud.Models;

namespace BarBud_Test;

public class IngredientPage
{
    private readonly IIngredientServices _ingredientServices = new IngredientServices();
    public IIngredientServices sut { get; set; }

    public IngredientPage()
    {
        sut = _ingredientServices;
    }

    private List<Ingredient> _fakeIngredients = new()
    {
        new Ingredient { Name = "Rum", Description = "light" },
        new Ingredient { Name = "Pomegranate", Description = "lots of seeds" },
        new Ingredient { Name = "Grenadine", Description = "red" }
    };

    [Fact]
    public void Delete_ShouldRemoveItemFromList()
    {
        // Arrange
        var ingredient = _fakeIngredients[0].Id;

        // Act
        sut.DeleteAsync(ingredient);

        // Assert
        Assert.DoesNotContain(_fakeIngredients, i => i.Id == ingredient);
    }
}