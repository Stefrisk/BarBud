using BarBud.Interfaces;
using BarBud.Services;
using BarBud.Models;
using Moq;
using BarBud.Db;
using Microsoft.EntityFrameworkCore;

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
    public async Task AddAsync_ShouldNotAddDuplicateIngredientNames()
    {
        // Arrange
        var duplicateIngredient = new Ingredient { Name = "Rum", Description = "dark" };
        var mockService = new Mock<IIngredientServices>();
        mockService.Setup(s => s.AddAsync(It.IsAny<Ingredient>()))
            .ReturnsAsync(_fakeIngredients)
            .Callback<Ingredient>(i =>
            {
                var alreadyExists = _fakeIngredients.Any(existing =>
                    string.Equals(existing.Name, i.Name, StringComparison.OrdinalIgnoreCase));
                if (!alreadyExists)
                {
                    _fakeIngredients.Add(i);
                }
            });

        // Act
        var result = await mockService.Object.AddAsync(duplicateIngredient);

        // Assert
        var rumCount = result.Count(i =>
            string.Equals(i.Name, duplicateIngredient.Name, StringComparison.OrdinalIgnoreCase));
        Assert.Equal(1, rumCount);
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

    [Fact]
    public async Task UpdateAsync_ShouldUpdateIngredientInList()
    {
        // Arrange
        var updatedIngredient = new Ingredient { Id = 1, Name = "White Rum", Description = "clear" };

        var mockService = new Mock<IIngredientServices>();
        mockService.Setup(s => s.UpdateAsync(It.IsAny<Ingredient>()))
            .ReturnsAsync(_fakeIngredients)
            .Callback<Ingredient>(i =>
            {
                var existing = _fakeIngredients.FirstOrDefault(x => x.Id == i.Id);
                if (existing != null)
                {
                    existing.Name = i.Name;
                    existing.Description = i.Description;
                }
            });

        // Act
        var result = await mockService.Object.UpdateAsync(updatedIngredient);

        // Assert
        var updated = result.First(i => i.Id == 1);

        Assert.Equal(updated.Name, _fakeIngredients.First(i => i.Id == 1).Name);
    }
}

public class IngredientIntegrationTests
{
    private DbContextOptions<BarBudDbContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<BarBudDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task AddAsync_ShouldPreventDuplicates_Integration()
    {
        // Arrange
        var options = CreateNewContextOptions();
        
        // Seed
        using (var context = new BarBudDbContext(options))
        {
            context.Ingredients.Add(new Ingredient { Id = 1, Name = "Rum", Description = "Light" });
            await context.SaveChangesAsync();
        }

        // Act
        using (var context = new BarBudDbContext(options))
        {
            var service = new IngredientFunctions(context);
            var duplicate = new Ingredient { Name = "Rum", Description = "Dark" };
            
            var result = await service.AddAsync(duplicate);

            // Assert
            Assert.Single(result);
            Assert.Equal("Rum", result.First().Name);
            Assert.Equal("Light", result.First().Description); // Should remain the original
        }
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveIngredient_Integration()
    {
        // Arrange
        var options = CreateNewContextOptions();

        // Seed
        using (var context = new BarBudDbContext(options))
        {
            context.Ingredients.Add(new Ingredient { Id = 1, Name = "Rum" });
            context.Ingredients.Add(new Ingredient { Id = 2, Name = "Mint" });
            await context.SaveChangesAsync();
        }

        // Act
        using (var context = new BarBudDbContext(options))
        {
            var service = new IngredientFunctions(context);
            var result = await service.DeleteAsync(1);

            // Assert
            Assert.Single(result);
            Assert.DoesNotContain(result, i => i.Id == 1);
            Assert.Contains(result, i => i.Id == 2);
        }
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateIngredient_Integration()
    {
        // Arrange
        var options = CreateNewContextOptions();

        // Seed
        using (var context = new BarBudDbContext(options))
        {
            context.Ingredients.Add(new Ingredient { Id = 1, Name = "Rum", Description = "Light" });
            await context.SaveChangesAsync();
        }

        // Act
        using (var context = new BarBudDbContext(options))
        {
            var service = new IngredientFunctions(context);
            var update = new Ingredient { Id = 1, Name = "Dark Rum", Description = "Aged" };
            
            var result = await service.UpdateAsync(update);

            // Assert
            var updated = result.FirstOrDefault(i => i.Id == 1);
            Assert.NotNull(updated);
            Assert.Equal("Dark Rum", updated.Name);
            Assert.Equal("Aged", updated.Description);
        }
    }
}