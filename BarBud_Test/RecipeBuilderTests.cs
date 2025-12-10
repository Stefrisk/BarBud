using Moq;
using BarBud.Models;
using BarBud.Interfaces;
using BarBud.Services;

namespace BarBud_Test;

public class RecipeBuilderTests
{
    private readonly Mock<IRecipeServices> _mockRecipeService;
    private readonly IRecipeBuilder _sut;

    public RecipeBuilderTests()
    {
        _mockRecipeService = new Mock<IRecipeServices>();
        _sut = new RecipeBuilder(_mockRecipeService.Object);
    }

    [Fact]
    public void Build_WithValidData_ShouldCreateRecipe()
    {
        // Arrange
        var drink = new Drink { Id = 1, Name = "Mojito" };

        // Act
        var recipe = _sut
            .ForDrink(drink)
            .WithName("Classic Mojito")
            .WithInstructions("Muddle mint with sugar and lime")
            .AddIngredient(1, 2, "oz")
            .Build();

        // Assert
        Assert.Equal("Classic Mojito", recipe.Name);
        Assert.Equal("Muddle mint with sugar and lime", recipe.Instructions);
        Assert.Equal(1, recipe.DrinkId);
        Assert.Single(recipe.Ingredients);
        Assert.Equal(2, recipe.Ingredients.First().Quantity);
        Assert.Equal("oz", recipe.Ingredients.First().Unit);
    }

    [Fact]
    public void ForDrink_WithNullDrink_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _sut.ForDrink((Drink)null!));
    }

    [Fact]
    public void ForDrink_WithInvalidDrinkId_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _sut.ForDrink(0));
        Assert.Throws<ArgumentException>(() => _sut.ForDrink(-1));
    }

    [Fact]
    public void WithName_WithEmptyString_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _sut.WithName(string.Empty));
        Assert.Throws<ArgumentException>(() => _sut.WithName("   "));
    }

    [Fact]
    public void AddIngredient_WithInvalidQuantity_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _sut.AddIngredient(1, 0, "oz"));
        Assert.Throws<ArgumentException>(() => _sut.AddIngredient(1, -1, "oz"));
    }

    [Fact]
    public void Build_WithoutName_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var drink = new Drink { Id = 1, Name = "Mojito" };

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() =>
            _sut.ForDrink(drink)
                .AddIngredient(1, 2, "oz")
                .Build()
        );

        Assert.Contains("Recipe name must be set", exception.Message);
    }

    [Fact]
    public void Build_WithoutDrink_ShouldThrowInvalidOperationException()
    {
        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() =>
            _sut.WithName("Classic Mojito")
                .AddIngredient(1, 2, "oz")
                .Build()
        );

        Assert.Contains("Drink must be set", exception.Message);
    }

    [Fact]
    public void Build_WithoutIngredients_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var drink = new Drink { Id = 1, Name = "Mojito" };

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() =>
            _sut.ForDrink(drink)
                .WithName("Classic Mojito")
                .Build()
        );

        Assert.Contains("Recipe must have at least one ingredient", exception.Message);
    }

    [Fact]
    public async Task BuildAndSaveAsync_ShouldCallRecipeService()
    {
        // Arrange
        var drink = new Drink { Id = 1, Name = "Mojito" };
        var expectedRecipe = new Recipe
        {
            Id = 1,
            Name = "Classic Mojito",
            DrinkId = 1,
            Drink = drink,
            Instructions = "Mix ingredients"
        };

        _mockRecipeService
            .Setup(s => s.AddAsync(It.IsAny<Recipe>()))
            .ReturnsAsync(expectedRecipe);

        // Act
        var result = await _sut
            .ForDrink(drink)
            .WithName("Classic Mojito")
            .WithInstructions("Mix ingredients")
            .AddIngredient(1, 2, "oz")
            .BuildAndSaveAsync();

        // Assert
        _mockRecipeService.Verify(s => s.AddAsync(It.Is<Recipe>(r =>
            r.Name == "Classic Mojito" &&
            r.DrinkId == 1 &&
            r.Ingredients.Count == 1
        )), Times.Once);
        
        Assert.Equal("Classic Mojito", result.Name);
    }

    [Fact]
    public void AddIngredients_WithMultipleIngredients_ShouldAddAll()
    {
        // Arrange
        var drink = new Drink { Id = 1, Name = "Mojito" };
        var ingredients = new List<RecipeIngredientInput>
        {
            new() { IngredientId = 1, Quantity = 2, Unit = "oz" },
            new() { IngredientId = 2, Quantity = 1, Unit = "oz" },
            new() { IngredientId = 3, Quantity = 10, Unit = "leaves" }
        };

        // Act
        var recipe = _sut
            .ForDrink(drink)
            .WithName("Classic Mojito")
            .AddIngredients(ingredients)
            .Build();

        // Assert
        Assert.Equal(3, recipe.Ingredients.Count);
        Assert.Contains(recipe.Ingredients, i => i.IngredientId == 1 && i.Quantity == 2);
        Assert.Contains(recipe.Ingredients, i => i.IngredientId == 2 && i.Quantity == 1);
        Assert.Contains(recipe.Ingredients, i => i.IngredientId == 3 && i.Quantity == 10);
    }

    [Fact]
    public void Reset_ShouldClearBuilderState()
    {
        // Arrange
        var drink = new Drink { Id = 1, Name = "Mojito" };
        _sut.ForDrink(drink)
            .WithName("Classic Mojito")
            .AddIngredient(1, 2, "oz");

        // Act
        _sut.Reset();

        // Assert - should throw because builder state is cleared
        Assert.Throws<InvalidOperationException>(() => _sut.Build());
    }

    [Fact]
    public void FluentAPI_ShouldReturnBuilder_ForChaining()
    {
        // Arrange
        var drink = new Drink { Id = 1, Name = "Mojito" };

        // Act - test that all methods return the builder
        var builder = _sut
            .ForDrink(drink)
            .WithName("Classic Mojito")
            .WithInstructions("Mix")
            .AddIngredient(1, 2, "oz");

        // Assert - verify we can still build
        var recipe = builder.Build();
        Assert.NotNull(recipe);
    }

    [Fact]
    public void WithName_ShouldTrimWhitespace()
    {
        // Arrange
        var drink = new Drink { Id = 1, Name = "Mojito" };

        // Act
        var recipe = _sut
            .ForDrink(drink)
            .WithName("  Classic Mojito  ")
            .AddIngredient(1, 2, "oz")
            .Build();

        // Assert
        Assert.Equal("Classic Mojito", recipe.Name);
    }

    [Fact]
    public void WithInstructions_WithNull_ShouldSetEmptyString()
    {
        // Arrange
        var drink = new Drink { Id = 1, Name = "Mojito" };

        // Act
        var recipe = _sut
            .ForDrink(drink)
            .WithName("Classic Mojito")
            .WithInstructions(null!)
            .AddIngredient(1, 2, "oz")
            .Build();

        // Assert
        Assert.Equal(string.Empty, recipe.Instructions);
    }
}